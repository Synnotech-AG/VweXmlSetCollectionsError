﻿using ExtendedXmlSerializer.Core.Sources;
using System.Reflection;

namespace ExtendedXmlSerializer.Configuration
{
	// ReSharper disable once UnusedTypeParameter
	public sealed class MemberInfo<T> : FixedInstanceSource<MemberInfo>
	{
		public MemberInfo(MemberInfo instance) : base(instance) {}
	}
}