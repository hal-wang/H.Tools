using Microsoft.Toolkit.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HTools.Uwp.Controls
{
    internal class FontSizeSelecterSource : IIncrementalSource<double>
    {
        private readonly double _minSize;
        private readonly double _maxSize;
        private readonly double _step;

        internal FontSizeSelecterSource(double minSize, double maxSize, double step)
        {
            _minSize = minSize;
            _maxSize = maxSize;
            _step = step;
        }

        public async Task<IEnumerable<double>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var result = new List<double>();

            for (var i = 0; i < pageSize; i++)
            {
                var value = pageIndex * pageSize * _step + _minSize + i * _step;

                if (value <= _maxSize)
                {
                    result.Add(value);
                }
                else
                {

                }
            }

            return await Task.FromResult(result);
        }
    }
}
