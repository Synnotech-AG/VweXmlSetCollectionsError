// MIT License
// 
// Copyright (c) 2016 Wojciech Nag�rski
//                    Michael DeMond
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections.Immutable;

namespace ExtendedXmlSerialization.Core.Sources
{
    public class Selector<TParameter, TResult> : WeakCacheBase<TParameter, TResult>, ISelector<TParameter, TResult>
        where TParameter : class where TResult : class
    {
        private readonly ImmutableArray<ICandidate<TParameter, TResult>> _candidates;

        public Selector(params ICandidate<TParameter, TResult>[] candidates) : this(candidates.ToImmutableArray()) {}

        public Selector(ImmutableArray<ICandidate<TParameter, TResult>> candidates)
        {
            _candidates = candidates;
        }

        protected override TResult Create(TParameter parameter)
        {
            foreach (var candidate in _candidates)
            {
                if (candidate.IsSatisfiedBy(parameter))
                {
                    return candidate.Get(parameter);
                }
            }
            return null;
        }
    }
}