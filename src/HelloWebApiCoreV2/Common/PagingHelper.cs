using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloApiWithCoreDapper.Common
{
    public class PagingHelper
    {
        public const string DapperSql2008ResultString = @"
            ;WITH _data AS (
                {0}
            ),
                _count AS (
                    SELECT COUNT(0) AS OverallCount FROM _data
            )
            SELECT * FROM (
                SELECT *, ROW_NUMBER() OVER (ORDER BY {1}) AS row_num FROM _data CROSS APPLY _count) x
            WHERE row_num BETWEEN {2}
                        AND {3}
                        ORDER BY {1}";
        public const string DapperSql2012ResultString = @"
            ;WITH _data AS (
                {0}
            ),
                _count AS (
                    SELECT COUNT(0) AS OverallCount FROM _data
            )
            SELECT * FROM _data CROSS APPLY _count ORDER BY {1} OFFSET {2} ROWS FETCH NEXT {3} ROWS ONLY";


        //public const string DapperSql2012ResultString = @"
        //    ;WITH _data AS (
        //        {0}
        //    ),
        //        _count AS (
        //            SELECT COUNT(0) AS OverallCount FROM _data
        //    )
        //    SELECT * FROM _data CROSS APPLY _count OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY";


        //public const string DapperSql2008ResultString = @"
        //    ;WITH _data AS (
        //        {0}
        //    ),
        //        _count AS (
        //            SELECT COUNT(0) AS OverallCount FROM _data
        //    )
        //    SELECT * FROM (
        //        SELECT *, ROW_NUMBER() OVER (ORDER BY {1}) AS row_num FROM _data CROSS APPLY _count) x
        //    WHERE row_num BETWEEN {2}
        //                AND {3}
        //                ORDER BY {1}";

    }
}
