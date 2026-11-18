import json
import threading
import unittest
import urllib.request
import urllib.error
from auth_probe import Server

class AuthFlow(unittest.TestCase):
    def test_local_auth_and_character_flow(self):
        with Server(('127.0.0.1', 0)) as srv:
            thread=threading.Thread(target=srv.serve_forever,daemon=True);thread.start()
            base='http://127.0.0.1:'+str(srv.server_address[1])
            def req(path,method='GET',body=None,token=None):
                headers={}
                if token: headers['Authorization']='Bearer '+token
                data=None if body is None else json.dumps(body).encode()
                try:
                    with urllib.request.urlopen(urllib.request.Request(base+path,data=data,headers=headers,method=method)) as r:return r.status,json.load(r)
                except urllib.error.HTTPError as e:return e.code,json.load(e)
            try:
                status, listing = req('/api/servers')
                self.assertEqual(status, 200)
                self.assertEqual(listing['servers'][0]['name'], 'FamHawaii')
                self.assertEqual(listing['servers'][0]['status'], 'offline')
                self.assertEqual(req('/api/servers/1/channels')[1]['channels'], [])
                self.assertEqual(req('/api/characters')[0],401)
                self.assertEqual(req('/api/auth/register','POST',{'username':'tester','password':'example','confirmPassword':'example'})[0],201)
                self.assertEqual(req('/api/auth/login','POST',{'username':'tester','password':'wrong'})[0],401)
                status,login=req('/api/auth/login','POST',{'username':'tester','password':'example'});self.assertEqual(status,200)
                token=login['token']
                self.assertEqual(req('/api/characters','POST',{'name':'Test','class':1,'school':1},token)[0],201)
                status,data=req('/api/characters',token=token);self.assertEqual(status,200);self.assertEqual(data['characters'][0]['name'],'Test')
                self.assertEqual(req('/api/auth/token','POST',{},token)[0],200)
                self.assertEqual(req('/api/characters/'+data['characters'][0]['id'],'DELETE',token=token)[0],200)
                self.assertEqual(req('/api/characters',token=token)[1]['characters'],[])
            finally:
                srv.shutdown();thread.join(3)

if __name__=='__main__':unittest.main()
