﻿using System;
using Cottle.Commons;

namespace Cottle.Scopes
{
	public sealed class DefaultScope : FallbackScope
	{
		#region Attributes

		private static volatile IScope	constant = null;

		private static readonly object	mutex = new object ();
		
		#endregion

		#region Constructors

		public	DefaultScope () :
			base (DefaultScope.GetConstant (), new SimpleScope ())
		{
		}

		#endregion

		#region Methods

		private static IScope	GetConstant ()
		{
			IScope	scope;

			if (DefaultScope.constant == null)
			{
				lock (DefaultScope.mutex)
				{
					if (DefaultScope.constant == null)
					{
						scope = new SimpleScope ();

						CommonFunctions.Assign (scope, ScopeMode.Closest);

						DefaultScope.constant = scope;
					}
				}
			}

			return DefaultScope.constant;
		}

		#endregion
	}
}