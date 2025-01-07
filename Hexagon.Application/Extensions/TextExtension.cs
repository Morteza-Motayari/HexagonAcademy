using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Extensions
{
    public static class TextExtension
    {
        public static string UserCommentShow(this string comment)
        {
            comment.Count();
            if(comment.Count() > 60)
            {
                comment = comment.Substring(0, 60);
                comment = comment + " ...";
            }
            return comment;
        }
    }
}
