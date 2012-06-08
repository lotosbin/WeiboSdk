﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using NetDimension.Weibo.Entities;
using Newtonsoft.Json;


namespace NetDimension.Weibo.Interface.Entity
{
	public class FriendshipInterface : WeiboInterface
	{
		public FriendshipInterface(Client client)
			: base(client)
		{

		}
		/// <summary>
		/// 获取用户的关注列表 
		/// </summary>
		/// <param name="uid">需要查询的用户UID。 </param>
		/// <param name="screenName">需要查询的用户昵称。 </param>
		/// <param name="count">单页返回的记录条数，默认为50，最大不超过200。</param>
		/// <param name="cursor">返回结果的游标，下一页用返回值里的next_cursor，上一页用previous_cursor，默认为0。</param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.user.Collection Friends(string uid = "", string screenName = "", int count = 50, int cursor = 0)
		{

			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.user.Collection>(Client.GetCommand("friendships/friends",
				string.IsNullOrEmpty(uid) ? new WeiboStringParameter("screen_name", screenName) : new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("cursor", cursor)));
		}
		/// <summary>
		/// 获取用户关注的用户UID列表
		/// </summary>
		/// <param name="uid">需要查询的用户UID。 </param>
		/// <param name="screenName">需要查询的用户昵称。 </param>
		/// <param name="count">单页返回的记录条数，默认为500，最大不超过5000。 </param>
		/// <param name="cursor">返回结果的游标，下一页用返回值里的next_cursor，上一页用previous_cursor，默认为0。</param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.user.IDCollection FriendIDs(string uid = "", string screenName = "", int count = 50, int cursor = 0)
		{
			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.user.IDCollection>(Client.GetCommand("friendships/friends/ids",
					string.IsNullOrEmpty(uid) ? new WeiboStringParameter("screen_name", screenName) : new WeiboStringParameter("uid", uid),
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("cursor", cursor)));
		}
		/// <summary>
		/// 获取两个用户之间的共同关注人列表 
		/// </summary>
		/// <param name="uid">需要获取共同关注关系的用户UID。</param>
		/// <param name="suid">需要获取共同关注关系的用户UID，默认为当前登录用户。</param>
		/// <param name="count">单页返回的记录条数，默认为50。 </param>
		/// <param name="page">返回结果的页码，默认为1。</param>
		/// <returns></returns>
		public IEnumerable<NetDimension.Weibo.Entities.user.Entity> FriendsInCommon(string uid = "", string suid = "", int count = 50, int page = 1)
		{
			return JsonConvert.DeserializeObject<IEnumerable<NetDimension.Weibo.Entities.user.Entity>>(Client.GetCommand("friendships/friends/in_common",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("suid", suid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}
		/// <summary>
		/// 获取用户的双向关注列表，即互粉列表 
		/// </summary>
		/// <param name="uid">需要获取双向关注列表的用户UID。 </param>
		/// <param name="count">单页返回的记录条数，默认为50。</param>
		/// <param name="page">返回结果的页码，默认为1。 </param>
		/// <param name="sort">排序类型，0：按关注时间最近排序，默认为0。</param>
		/// <returns></returns>
		public IEnumerable<NetDimension.Weibo.Entities.user.Entity> FriendsOnBilateral(string uid, int count = 50, int page = 1, bool sort = false)
		{
			return JsonConvert.DeserializeObject<IEnumerable<NetDimension.Weibo.Entities.user.Entity>>(Client.GetCommand("friendships/friends/bilateral",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page),
				new WeiboStringParameter("sort", sort)));
		}
		/// <summary>
		/// 获取用户双向关注的用户ID列表，即互粉UID列表 
		/// </summary>
		/// <param name="uid">需要获取双向关注列表的用户UID。</param>
		/// <param name="count">单页返回的记录条数，默认为50，最大不超过2000。 </param>
		/// <param name="page">返回结果的页码，默认为1。</param>
		/// <param name="sort">排序类型，0：按关注时间最近排序，默认为0。 </param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.user.IDCollection FriendsOnBilateralIDs(string uid, int count = 50, int page = 1, bool sort = false)
		{
			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.user.IDCollection>(Client.GetCommand("friendships/friends/bilateral/ids",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page),
				new WeiboStringParameter("sort", sort)));
		}
		/// <summary>
		/// 获取用户的粉丝列表 
		/// </summary>
		/// <param name="uid">需要查询的用户UID。 </param>
		/// <param name="screenName">需要查询的用户昵称。 </param>
		/// <param name="count">单页返回的记录条数，默认为50，最大不超过200。</param>
		/// <param name="cursor">返回结果的游标，下一页用返回值里的next_cursor，上一页用previous_cursor，默认为0。</param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.user.Collection Followers(string uid = "", string screenName = "", int count = 50, int cursor = 0)
		{
			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.user.Collection>(Client.GetCommand("friendships/followers",
				string.IsNullOrEmpty(uid) ? new WeiboStringParameter("screen_name", screenName) : new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("cursor", cursor)));
		}
		/// <summary>
		/// 获取用户粉丝的用户UID列表 
		/// </summary>
		/// <param name="uid">需要查询的用户UID。</param>
		/// <param name="screenName">需要查询的用户昵称。 </param>
		/// <param name="count">单页返回的记录条数，默认为500，最大不超过5000。</param>
		/// <param name="cursor">返回结果的游标，下一页用返回值里的next_cursor，上一页用previous_cursor，默认为0。 </param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.user.IDCollection FollowerIDs(string uid = "", string screenName = "", int count = 50, int cursor = 0)
		{
			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.user.IDCollection>(Client.GetCommand("friendships/followers/ids",
				string.IsNullOrEmpty(uid) ? new WeiboStringParameter("screen_name", screenName) : new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("cursor", cursor)));
		}
		/// <summary>
		/// 获取用户的活跃粉丝列表
		/// </summary>
		/// <param name="uid">需要查询的用户UID。 </param>
		/// <param name="count">返回的记录条数，默认为20，最大不超过200。 </param>
		/// <returns></returns>
		public IEnumerable<NetDimension.Weibo.Entities.user.Entity> FollowersInActive(string uid, int count = 20)
		{
			return JsonConvert.DeserializeObject<IEnumerable<NetDimension.Weibo.Entities.user.Entity>>(Client.GetCommand("friendships/followers/active",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count)));

		}
		/// <summary>
		/// 获取当前登录用户的关注人中又关注了指定用户的用户列表
		/// </summary>
		/// <param name="uid">指定的关注目标用户UID。 </param>
		/// <param name="count">单页返回的记录条数，默认为50。 </param>
		/// <param name="page">返回结果的页码，默认为1。</param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.user.Collection FriendsChain(string uid, int count = 50, int page = 1)
		{
			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.user.Collection>(Client.GetCommand("friendships/friends_chain/followers",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}
		/// <summary>
		/// 获取两个用户之间的详细关注关系情况
		/// </summary>
		/// <param name="sourceID">源用户的UID。</param>
		/// <param name="sourceScreenName">源用户的微博昵称。 </param>
		/// <param name="targetID">目标用户的UID。 </param>
		/// <param name="targetScreenName">目标用户的微博昵称。 </param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.friendship.Result Show(string sourceID = "", string sourceScreenName = "", string targetID = "", string targetScreenName = "")
		{
			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.friendship.Result>(Client.GetCommand("friendships/show",
				string.IsNullOrEmpty(sourceID) ? new WeiboStringParameter("source_screen_name", sourceScreenName) : new WeiboStringParameter("source_id", sourceID),
				string.IsNullOrEmpty(targetID) ? new WeiboStringParameter("target_screen_name", targetScreenName) : new WeiboStringParameter("uid", targetID)));
		}
		/// <summary>
		/// 关注一个用户 
		/// </summary>
		/// <param name="uid">需要关注的用户ID。</param>
		/// <param name="screenName">需要关注的用户昵称。 </param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.user.Entity Create(string uid = "", string screenName = "")
		{
			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.user.Entity>(Client.PostCommand("friendships/create",
				string.IsNullOrEmpty(uid) ? new WeiboStringParameter("screen_name", screenName) : new WeiboStringParameter("uid", uid)));
		}
		/// <summary>
		/// 取消关注一个用户 
		/// </summary>
		/// <param name="uid">需要取消关注的用户ID。</param>
		/// <param name="screenName">需要取消关注的用户昵称。 </param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.user.Entity Destroy(string uid = "", string screenName = "")
		{
			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.user.Entity>(Client.PostCommand("friendships/destroy",
				string.IsNullOrEmpty(uid) ? new WeiboStringParameter("screen_name", screenName) : new WeiboStringParameter("uid", uid)));
		}
		/// <summary>
		/// 更新当前登录用户所关注的某个好友的备注信息 
		/// </summary>
		/// <param name="uid">需要修改备注信息的用户UID。 </param>
		/// <param name="remark">备注信息</param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.user.Entity UpdateRemark(string uid, string remark)
		{
			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.user.Entity>(Client.PostCommand("friendships/remark/update",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("remark", remark)));
		}
	}
}