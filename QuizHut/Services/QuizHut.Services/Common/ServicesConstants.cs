﻿namespace QuizHut.Services.Common
{
    public static class ServicesConstants
    {

        public const string RedisModelKey = "quiz";
        public const string RightAnswerValue = "True";
        public const string AnswerNumberPattern = @"[\d]";
        public const int NoPointsValue = 0;
        public const int PointsValue = 1;
        public const int ValueCollectionMinCount = 1;
    }
}