﻿// <summary>
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </summary>

using System.Threading.Tasks;

namespace org.apache.zookeeper.client
{

	/// <summary>
	/// A set of hosts a ZooKeeper client should connect to.
	/// 
	/// Classes implementing this interface must guarantee the following:
	/// 
    /// * Every call to next() returns an IPEndPoint. So the iterator never
	/// ends.
	/// 
	/// * The size() of a HostProvider may never be zero.
	/// 
    /// A HostProvider must return resolved IPEndPoint instances on next(),
	/// but it's up to the HostProvider, when it wants to do the resolving.
	/// 
	/// Different HostProvider could be imagined:
	/// 
	/// * A HostProvider that loads the list of Hosts from an URL or from DNS 
    /// * A HostProvider that re-resolves the IPEndPoint after a timeout. 
	/// * A HostProvider that prefers nearby hosts.
	/// </summary>
	internal interface HostProvider
	{
		int size();

	    /// <summary>
	    /// The next host to try to connect to.
	    /// 
	    /// For a spinDelay of 0 there should be no wait.
	    /// </summary>
	    /// <param name="spinDelay">
	    ///     Milliseconds to wait if all hosts have been tried once. </param>
	    Task<ResolvedEndPoint> next(int spinDelay);

		/// <summary>
		/// Notify the HostProvider of a successful connection.
		/// 
		/// The HostProvider may use this notification to reset it's inner state.
		/// </summary>
		void onConnected();
	}

}