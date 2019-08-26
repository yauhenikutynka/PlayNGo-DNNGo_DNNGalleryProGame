using System;
using System.Collections.Generic;
using System.Web;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Common.Utilities;

namespace DNNGo.Modules.DNNGalleryProGame
{
    /// <summary>
    /// 扩展用户当前时间
    /// </summary>
    public class xUserTime //: UserTime
    {

        public static DateTime LocalTime()
        {
            return LocalTime(UtcTime());
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Convert utc time in User's timezone
        /// </summary>
        /// <param name="utcTime">Utc time to convert</param>
        /// -----------------------------------------------------------------------------       
        public static DateTime LocalTime(DateTime utcTime)
        {
            var PortalSettings = PortalController.Instance.GetCurrentPortalSettings();
            var TimeZone = TimeZoneInfo.Local;
            //if (IsDaylightSavingTime)//如果是夏令时
            //{
            //    utcTime = utcTime.AddMinutes(-60);
            //}

            if (PortalSettings != null)
            {
                if (PortalSettings.UserId > Null.NullInteger)
                {
                    TimeZone = PortalSettings.UserInfo.Profile.PreferredTimeZone;
                    return TimeZoneInfo.ConvertTime(utcTime, TimeZoneInfo.Utc, TimeZone);
                }
                else
                {
                    TimeZone = PortalController.Instance.GetCurrentPortalSettings().TimeZone;
                    return TimeZoneInfo.ConvertTime(utcTime, TimeZoneInfo.Utc, TimeZone);
                }
            }
            return TimeZoneInfo.ConvertTime(utcTime, TimeZoneInfo.Utc, TimeZone);

        }

        /// <summary>
        /// UTC 时间 (取自数据库时间)
        /// </summary>
        /// <returns></returns>
        public static DateTime UtcTime()
        {
            return DateUtils.GetDatabaseTime();
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Convert utc time in User's timezone
        /// </summary>
        /// <param name="utcTime">Utc time to convert</param>
        /// -----------------------------------------------------------------------------       
        public static DateTime ServerTime(DateTime localTime)
        {
            var PortalSettings = PortalController.Instance.GetCurrentPortalSettings();
            var TimeZone = TimeZoneInfo.Local;
            //if (IsDaylightSavingTime)//如果是夏令时
            //{
            //    localTime = localTime.AddMinutes(60);
            //}

            if (PortalSettings != null)
            {
                if (PortalSettings.UserId > Null.NullInteger)
                {
                    TimeZone = PortalSettings.UserInfo.Profile.PreferredTimeZone;
                    return TimeZoneInfo.ConvertTimeToUtc(localTime, TimeZone);
                }
                else
                {
                    TimeZone = PortalController.Instance.GetCurrentPortalSettings().TimeZone;
                    return TimeZoneInfo.ConvertTimeToUtc(localTime, TimeZone);
                }

            }
            return TimeZoneInfo.ConvertTimeToUtc(localTime, TimeZone);

        }

        public static DateTime ServerTime()
        {
            return ServerTime(DateTime.Now);
        }








        /// <summary>
        /// 返回一个值，用以指示指定日期和时间是否处于夏时制期间
        /// </summary>
        public static Boolean IsDaylightSavingTime
        {
            get
            {

                TimeZoneHelper.TimeZoneInformation tz = TimeZoneHelper.GetTimeZone();


                return tz.daylightName != tz.standardName && tz.daylightBias != 0 && IsNowAMESDayLightSavingTime;
            }
        }

        /// <summary> 
        /// 判断当前日期是否是美国夏令时 
        /// 从2007年开始每年3月的第二个星期日开始夏令时，结束日期为11月的第一个星期日。 
        /// </summary> 
        /// <returns>是，返回true，否则为false</returns> 
        public static bool IsNowAMESDayLightSavingTime
        {
            get
            {
                return DateTime.UtcNow > DayLightSavingStartTimeUtc
                    && DateTime.UtcNow < DayLightSavingEndTimeUtc;
            }
        }



        private static DateTime _thisYearDaylightSavingTimeStart, _thisYearDaylightSavingTimeEnd;

        private const int TIMEZONE_OFFSET_DAY_SAVING_LIGHT = -12;
        private const int TIMEZONE_OFFSET = -13;

        /// <summary> 
        /// 夏令时开始时间 
        /// </summary> 
        static DateTime DayLightSavingStartTimeUtc
        {
            get
            {
                if (_thisYearDaylightSavingTimeStart.Year != DateTime.Now.Year)
                {
                    DateTime temp = new DateTime(DateTime.Now.Year, 3, 8, 0, 0, 0);
                    while (temp.DayOfWeek != DayOfWeek.Sunday)
                    {
                        temp = temp.AddDays(1);
                    }
                    _thisYearDaylightSavingTimeStart = temp.AddHours(TIMEZONE_OFFSET);
                }

                return _thisYearDaylightSavingTimeStart;
            }
        }

        /// <summary> 
        /// 夏令时结束时间 
        /// </summary> 
        static DateTime DayLightSavingEndTimeUtc
        {
            get
            {
                if (_thisYearDaylightSavingTimeEnd.Year != DateTime.Now.Year)
                {
                    DateTime temp = new DateTime(DateTime.Now.Year, 11, 1, 0, 0, 0);
                    while (temp.DayOfWeek != DayOfWeek.Sunday)
                    {
                        temp = temp.AddDays(1);
                    }
                    _thisYearDaylightSavingTimeEnd = temp.AddHours(TIMEZONE_OFFSET_DAY_SAVING_LIGHT);
                }
                return _thisYearDaylightSavingTimeEnd;
            }
        }

    }
}