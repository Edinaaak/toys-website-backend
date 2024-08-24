using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Contracts.Models.Masterpiece.Response
{
    public class GetReviewsResponse
    {
       
        public int ReviewsForFive { get; set; }
        public int ReviewsForFour { get; set; }
        public int ReviewsForThree { get; set; }
        public int ReviewsForTwo { get; set; }
        public int ReviewsForOne { get; set; }
        public int TotalReviews { get; set; }

    }
}