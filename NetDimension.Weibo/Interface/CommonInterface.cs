﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Codeplex.Data;

namespace NetDimension.Weibo.Interface
{
	public class CommonInterface: WeiboInterface
	{
		public CommonInterface(Client client)
			: base(client)
		{

		}
		/// <summary>
		/// 通过地址编码获取地址名称 
		/// </summary>
		/// <param name="codes">需要查询的地址编码</param>
		/// <returns></returns>
		public dynamic CodeToLocation(params string[] codes)
		{
			return DynamicJson.Parse(Client.GetCommand("common/code_to_location", new WeiboStringParameter("codes", string.Join(",",codes))));
		}
		/// <summary>
		/// 获取城市列表
		/// </summary>
		/// <param name="province">省份的省份代码。</param>
		/// <param name="capital">城市的首字母，a-z，可为空代表返回全部，默认为全部。</param>
		/// <returns></returns>
		public dynamic GetCity(string province,string capital="")
		{
			return DynamicJson.Parse(Client.GetCommand("common/get_city", 
				new WeiboStringParameter("province", province),
				new WeiboStringParameter("capital", capital)));
		}
		/// <summary>
		/// 获取省份列表 
		/// </summary>
		/// <param name="country">国家的国家代码。</param>
		/// <param name="capital">省份的首字母，a-z，可为空代表返回全部，默认为全部。 </param>
		/// <returns></returns>
		public dynamic GetProvince(string country, string capital = "")
		{
			return DynamicJson.Parse(Client.GetCommand("common/get_province",
				new WeiboStringParameter("country", country),
				new WeiboStringParameter("capital", capital)));
		}
		/// <summary>
		/// 获取国家列表 
		/// </summary>
		/// <param name="capital">国家的首字母，a-z，可为空代表返回全部，默认为全部。</param>
		/// <returns></returns>
		public dynamic GetCountry(string capital = "")
		{
			return DynamicJson.Parse(Client.GetCommand("common/get_country",
				new WeiboStringParameter("capital", capital)));
		}
		/// <summary>
		/// 获取时区配置表
		/// </summary>
		/// <returns></returns>
		public dynamic GetTimezone()
		{
			return DynamicJson.Parse(Client.GetCommand("common/get_timezone"));
		}

	}
}
