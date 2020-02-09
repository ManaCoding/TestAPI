using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi.Extension
{
    public static class PathStringExtension
    {
        public static string[] GetSegments(this PathString pathString)
        {
			string[] segments;

			if (!pathString.HasValue)
			{
				segments = new string[0];
			}
			else
			{
				string path = pathString;
				ArrayList pathSegments = new ArrayList();
				int current = 1;
				while (current < path.Length)
				{
					int next = path.IndexOf('/', current);
					if (next == -1)
					{
						next = path.Length;
					}
					pathSegments.Add(path.Substring(current - 1, (next - current) + 1));
					current = next + 1;
				}
				segments = (string[])(pathSegments.ToArray(typeof(string)));
			}

			return segments;
		}
    }
}
