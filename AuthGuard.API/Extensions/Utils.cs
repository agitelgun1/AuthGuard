using System.Collections.Generic;

namespace AuthGuard.API.Extensions
{
    public static class Utils
    {
        public static bool CompareLists(this List<string> sourceList, List<string> destinationList)
        {
            if (sourceList.Count != destinationList.Count)
                return false;

            for (int i = 0; i < sourceList.Count; i++)
            {
                if (sourceList[i] != destinationList[i])
                    return false;
            }

            return true;
        }
    }
}