//======================================================================
//        Copyright (C) 2008-2009 Conan    
//        All rights reserved
//
//        Filename :MyRequest
//        Description :该类主要用于C/S程序模拟Web请求
//
//        Created by Conan at  2009-3-6 16:39:35
//        http://conan87810.cnblogs.com      
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace MyUtility.CS
{
    
   public class MyRequest
    {
       public MyRequest() { }


       /// <summary>
       /// 通过HttpWebRequest 发送请求(表单)
       /// </summary>
       /// <param name="url">请求地址</param>
       /// <param name="requestParameter">请求参数para1=value1&para2=value2</param>
       /// <param name="method">post get</param>
       /// <param name="encoding">GB2312 UTF-8...</param>
       /// <returns>响应回返字符串</returns>
       public string SubmitRequest(string url, string requestParameter, string requestMethod, Encoding encoding)
       {
           string result = String.Empty;
           #region Request部分
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);//新建一个HttpWebRequest
           myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
           myHttpWebRequest.ContentLength = requestParameter.Length;
           myHttpWebRequest.Method = requestMethod;
           Stream myRequestStream = myHttpWebRequest.GetRequestStream();//获取Request流
           StreamWriter myStreamWriter = new StreamWriter(myRequestStream,encoding);
           myStreamWriter.Write(requestParameter);  //把参数写入HttpWebRequest的Request流 
           myStreamWriter.Close();
           myRequestStream.Close();  //关闭打开对象 
           #endregion
           HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse(); //提交获得响应
           #region Response部分
           Stream myResponseStream = myHttpWebResponse.GetResponseStream();//获取Response流
           StreamReader myStreamReader = new StreamReader(myResponseStream, encoding);
           result = myStreamReader.ReadToEnd();//把数据从HttpWebResponse的Response流中读出 
           myStreamReader.Close();
           myResponseStream.Close();
           #endregion
           return result;
       
       }


       /// <summary>
       /// 请求并获得cookie(表单)
       /// </summary>
       /// <param name="url">请求地址</param>
       /// <param name="requestParameter">请求参数para1=value1&para2=value2</param>
       /// <param name="requestMethod">post get</param>
       /// <param name="encoding">GB2312 UTF-8...</param>
       /// <param name="response">响应回返字符串</param>
       /// <returns>CookieContainer</returns>
       public CookieContainer SubmitRequest(string url, string requestParameter, string requestMethod, Encoding encoding,out string response)
       {

           CookieContainer cookie = new CookieContainer(); //新建一个CookieContainer来存放Cookie集合 
           #region Request部分
           HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);//新建一个HttpWebRequest
           myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
           myHttpWebRequest.ContentLength = requestParameter.Length;
           myHttpWebRequest.Method = requestMethod;
           myHttpWebRequest.CookieContainer = cookie; //设置HttpWebRequest的CookieContainer///////
           Stream myRequestStream = myHttpWebRequest.GetRequestStream();//获取Request流
           StreamWriter myStreamWriter = new StreamWriter(myRequestStream, encoding);
           myStreamWriter.Write(requestParameter);  //把参数写入HttpWebRequest的Request流 
           myStreamWriter.Close();
           myRequestStream.Close();  //关闭打开对象 
           #endregion
           HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse(); //提交获得响应
           #region Response部分
           Stream myResponseStream = myHttpWebResponse.GetResponseStream();//获取Response流
           StreamReader myStreamReader = new StreamReader(myResponseStream, encoding);
           response = myStreamReader.ReadToEnd();//把数据从HttpWebResponse的Response流中读出 
           myStreamReader.Close();
           myResponseStream.Close();
           #endregion

           return cookie;
       
       
       }




    }
}
