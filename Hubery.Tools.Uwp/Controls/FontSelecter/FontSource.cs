using Hubery.Common.Base;
using Microsoft.Toolkit.Collections;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Hubery.Common.Uwp.Controls.FontSelecter
{
    internal class FontSource : IIncrementalSource<string>
    {
        private readonly SharpDX.DirectWrite.Factory _factory = new SharpDX.DirectWrite.Factory();

        public async Task<IEnumerable<string>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var result = new List<string>();

            await TaskExtend.Run(() =>
            {
                using (var fontCollection = _factory.GetSystemFontCollection(false))
                {
                    int count = Math.Min(fontCollection.FontFamilyCount, (pageIndex + 1) * pageSize);
                    for (int i = pageIndex * pageSize; i < count; i++)
                    {
                        var fontFamily = fontCollection.GetFontFamily(i);

                        if (!fontFamily.FamilyNames.FindLocaleName(CultureInfo.CurrentCulture.Name, out int index))
                            index = 0;

                        result.Add(fontFamily.FamilyNames.GetString(index));
                    }
                };
                Thread.Sleep(100);
            });

            return result;
        }
    }
}
