﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NetDimension.Weibo.Entities.search
{
	public class School
	{
		[JsonProperty("school_name")]
		public string Name { get; internal set; }
		[JsonProperty("location")]
		public string Location { get; internal set; }
		[JsonProperty("id")]
		public string ID { get; internal set; }
		[JsonProperty("type")]
		public string Type { get; internal set; }
	}
}