/*
 * 由SharpDevelop创建。
 * 用户： yondor_74
 * 日期: 2018/10/22
 * 时间: 10:31
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO;
using System.Collections.Generic;
using Baidu.Aip;
using System.Text.RegularExpressions;

namespace BaiduOCR
{
	class Program
	{
		private static Baidu.Aip.Ocr.Ocr client;
		public static void Main(string[] args)
		{
			Console.WriteLine("百度OCR工具");
			
			// TODO: Implement Functionality Here
			
			var APP_ID = "YOUR_APP_ID";
			var API_KEY = "YOUR_API_KEY";
			var SECRET_KEY = "YOUR_SECRET_KEY";
			
			client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
			client.Timeout = 60000;  // 修改超时时间					
			
			String path = "";
			int type = 0;
			String typeStr = "general";
			
			Regex reg = new Regex(".(jpg|png|jpeg)");
			
			
			while(true){
				
				
				Console.WriteLine("请输入图片路径，不输入的场合沿用上一张图片:");
				String tmp = Console.ReadLine();
				if(tmp!=null && tmp.Length > 0){
					path = tmp;
				}
				
				Console.WriteLine("输入ocr类型，0=普通，1=高精度，不输入的场合沿用上一种类型:");
				tmp = Console.ReadLine();				
				if(tmp!=null && tmp.Length > 0){
					type = int.Parse(tmp);
				}

				Console.WriteLine("识别图片： "+path);			
				
				String result = "";
				
				if(type==0){
					typeStr = "general";
					result = GeneralBasicDemo(path);
				}else if(type==1){
					typeStr = "accurate";
					result = AccurateBasicDemo(path);					
				}
				
				String rpath = reg.Replace(path , "_"+typeStr+".json");
				File.WriteAllText(rpath , result);
				
				Console.WriteLine("Press any key to continue . . . ");
			}
			
			
			Console.ReadKey(true);
		}
		
		public static  String GeneralBasicDemo(String p) {
			Console.WriteLine("通用文字识别");
			var image = File.ReadAllBytes(p);
			// 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
			var result = client.GeneralBasic(image);
			Console.WriteLine(result);
			// 如果有可选参数
			var options = new Dictionary<string, object>{
			    {"language_type", "CHN_ENG"},
			    {"detect_direction", "true"},
			    {"detect_language", "true"},
			    {"probability", "true"}
			};
			// 带参数调用通用文字识别, 图片参数为本地图片
			result = client.GeneralBasic(image, options);			
			Console.WriteLine(result);
			return result.ToString();;
		}
		
		public static String AccurateBasicDemo(String p) {
			Console.WriteLine("通用文字识别（高精度版）");
			var image = File.ReadAllBytes(p);
			// 调用通用文字识别（高精度版），可能会抛出网络等异常，请使用try/catch捕获
			var result = client.AccurateBasic(image);
			Console.WriteLine(result);
			// 如果有可选参数
			var options = new Dictionary<string, object>{
			    {"detect_direction", "true"},
			    {"probability", "true"}
			};
			// 带参数调用通用文字识别（高精度版）
			result = client.AccurateBasic(image, options);
			Console.WriteLine(result);
			return result.ToString();
		}
		
		
	}
}