﻿using System;
using System.Linq;
using System.Reflection;
using ExtendedXmlSerializer.Core.Sources;
using ExtendedXmlSerializer.ReflectionModel;

namespace ExtendedXmlSerializer.Core.Specifications
{
	public static class Extensions
	{
		public static ISpecification<TParameter> IfAssigned<TParameter, TResult>(
			this IParameterizedSource<TParameter, TResult> @this)
			=> @this.ToDelegate()
			        .IfAssigned();

		public static ISpecification<TParameter> IfAssigned<TParameter, TResult>(this Func<TParameter, TResult> @this)
			=> new DelegatedAssignedSpecification<TParameter, TResult>(@this);

		public static Func<T, bool> ToDelegate<T>(this ISpecification<T> @this) => @this.IsSatisfiedBy;

		public static Func<bool> Build<T>(this ISpecification<TypeInfo> @this) => @this.ToDelegate()
		                                                                               .Build(Support<T>.Key);

		public static Func<bool> Build<T>(this ISpecification<T> @this, T parameter) => @this.ToDelegate()
		                                                                                     .Build(parameter);

		public static Func<bool> Fix<T>(this ISpecification<T> @this, T parameter) => @this.ToDelegate()
		                                                                                   .Fix(parameter)
		                                                                                   .ToDelegate();

		public static ISpecification<T> Any<T>(this ISpecification<T> @this, params T[] parameters)
			=> new AnySpecification<T>();

		public static ISpecification<T> Or<T>(this ISpecification<T> @this, params ISpecification<T>[] others)
			=> new AnySpecification<T>(@this.Yield()
			                                .Concat(others)
			                                .Fixed());

		public static ISpecification<T> And<T>(this ISpecification<T> @this, params ISpecification<T>[] others)
			=> new AllSpecification<T>(@this.Yield()
			                                .Concat(others)
			                                .Fixed());

		public static ISpecification<T> Inverse<T>(this ISpecification<T> @this) => new InverseSpecification<T>(@this);

		public static ISpecification<object> AdaptForNull<T>(this ISpecification<T> @this)
			=> new NullAwareSpecificationAdapter<T>(@this);

		public static ISpecification<object> Adapt<T>(this ISpecification<T> @this)
			=> new SpecificationAdapter<T>(@this);
	}
}