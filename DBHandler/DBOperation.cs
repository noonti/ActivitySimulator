using Dapper;
using DBHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHandler
{
    public class DBOperation
    {
        private readonly DapperORM _dapperOrm = new DapperORM();

        public IEnumerable<SENSOR> GetSensorList(out SP_RESULT spResult)
        {
            var param = new DynamicParameters();
            param.AddDynamicParams(new
            {
            });
            return _dapperOrm.ReturnList<SENSOR>("SP_GET_SENSOR_LIST", param, out spResult).ToList();

        }


        public IEnumerable<LifeLog48> GetLifelog48List(out SP_RESULT spResult)
        {
            var param = new DynamicParameters();
            param.AddDynamicParams(new
            {
            });
            return _dapperOrm.ReturnList<LifeLog48>("SP_GET_IL_LIFELOG_48H_LIST", param, out spResult).ToList();

        }


        public void AddLifelog48(LifeLog48 data, out SP_RESULT spResult)
        {
            var param = new DynamicParameters();
            param.AddDynamicParams(new
            {
                I_care_recipient_id= data.care_recipient_id,
                I_lifelog_date_1_list = data.lifelog_date_1_list,
                I_validation_1_list = data.validation_1_list,
                I_place_1_list = data.place_1_list,
                I_AIX_1_list = data.AIX_1_list,
                I_lifelog_date_h_list = data.lifelog_date_h_list,
                I_validation_h_list = data.validation_h_list,
                I_AIX_h_list = data.AIX_h_list,
                I_place_code_h_p1_list = data.place_code_h_p1_list,
                I_place_code_h_p2_list = data.place_code_h_p2_list,
                I_place_code_h_p3_list = data.place_code_h_p3_list,
                I_place_code_h_p4_list = data.place_code_h_p4_list,
                I_place_code_h_p5_list = data.place_code_h_p5_list,
                I_place_code_h_p6_list = data.place_code_h_p6_list,
                I_place_code_h_p7_list = data.place_code_h_p7_list,
                I_place_code_h_p8_list = data.place_code_h_p8_list,
                I_place_code_h_p9_list = data.place_code_h_p9_list,
                I_place_code_h_p10_list = data.place_code_h_p10_list,
                I_AIX_h_p1_list = data.AIX_h_p1_list,
                I_AIX_h_p2_list = data.AIX_h_p2_list,
                I_AIX_h_p3_list = data.AIX_h_p3_list,
                I_AIX_h_p4_list = data.AIX_h_p4_list,
                I_AIX_h_p5_list = data.AIX_h_p5_list,
                I_AIX_h_p6_list = data.AIX_h_p6_list,
                I_AIX_h_p7_list = data.AIX_h_p7_list,
                I_AIX_h_p8_list = data.AIX_h_p8_list,
                I_AIX_h_p9_list = data.AIX_h_p9_list,
                I_AIX_h_p10_list = data.AIX_h_p10_list,
                I_lifelog_date_d_list = data.lifelog_date_d_list,
                I_validation_d_list = data.validation_d_list,
                I_AIX_d_list = data.AIX_d_list,
                I_place_code_d_p1_list = data.place_code_d_p1_list,
                I_place_code_d_p2_list = data.place_code_d_p2_list,
                I_place_code_d_p3_list = data.place_code_d_p3_list,
                I_place_code_d_p4_list = data.place_code_d_p4_list,
                I_place_code_d_p5_list = data.place_code_d_p5_list,
                I_place_code_d_p6_list = data.place_code_d_p6_list,
                I_place_code_d_p7_list = data.place_code_d_p7_list,
                I_place_code_d_p8_list = data.place_code_d_p8_list,
                I_place_code_d_p9_list = data.place_code_d_p9_list,
                I_place_code_d_p10_list = data.place_code_d_p10_list,
                I_AIX_d_p1_list = data.AIX_d_p1_list,
                I_AIX_d_p2_list = data.AIX_d_p2_list,
                I_AIX_d_p3_list = data.AIX_d_p3_list,
                I_AIX_d_p4_list = data.AIX_d_p4_list,
                I_AIX_d_p5_list = data.AIX_d_p5_list,
                I_AIX_d_p6_list = data.AIX_d_p6_list,
                I_AIX_d_p7_list = data.AIX_d_p7_list,
                I_AIX_d_p8_list = data.AIX_d_p8_list,
                I_AIX_d_p9_list = data.AIX_d_p9_list,
                I_AIX_d_p10_list = data.AIX_d_p10_list,
                I_AIX_1Eq0RepeatCount = data.AIX_1Eq0RepeatCount,
                I_AIX_1Gt0RepeatCount = data.AIX_1Gt0RepeatCount,
                I_sleep_depth_1_list = data.sleep_depth_1_list,
                I_outgoing_1_list = data.outgoing_1_list,
                I_total_AIX_h_list = data.total_AIX_h_list,
                I_total_AIX_h_count_list = data.total_AIX_h_count_list,
                I_total_AIX_d_list = data.total_AIX_d_list,
                I_total_AIX_d_count_List = data.total_AIX_d_count_List,
                I_total_sleep_start_time_d_list = data.total_sleep_start_time_d_list,
                I_total_sleep_start_time_d_count_list = data.total_sleep_start_time_d_count_list,
                I_total_sleep_end_time_d_list = data.total_sleep_end_time_d_list,
                I_total_sleep_end_time_d_count_list = data.total_sleep_end_time_d_count_list,
                I_total_bath_count_d_list = data.total_bath_count_d_list,
                I_total_bath_count_d_count_list = data.total_bath_count_d_count_list,
                I_total_bath_time_d_list = data.total_bath_time_d_list,
                I_total_bath_time_d_count_list = data.total_bath_time_d_count_list,
                I_total_outgoing_count_d_list = data.total_outgoing_count_d_list,
                I_total_outgoing_count_d_count_list = data.total_outgoing_count_d_count_list,
                I_total_outgoing_time_d_list = data.total_outgoing_time_d_list,
                I_total_outgoing_time_d_count_list = data.total_outgoing_time_d_count_list

            });
            _dapperOrm.ExecuteWithoutReturn("SP_ADD_IL_LIFELOG_48H", param, out spResult);
        }

        public IEnumerable<OUT_LOG> GetOutLogList(OUT_LOG data, out SP_RESULT spResult)
        {
            var param = new DynamicParameters();
            param.AddDynamicParams(new
            {
                I_USER_ID = data.userId,
                I_START_DATE = data.startDate,
                I_END_DATE = data.endDate,
            });
            return _dapperOrm.ReturnList<OUT_LOG>("SP_GET_OUT_LOG_LIST", param, out spResult).ToList();

        }


        public IEnumerable<OUT_LOG> GetLifelogOutLogList(OUT_LOG data, out SP_RESULT spResult)
        {
            var param = new DynamicParameters();
            param.AddDynamicParams(new
            {
                I_USER_ID = data.userId,
                I_START_DATE = data.startDate,
                I_END_DATE = data.endDate,
            });
            return _dapperOrm.ReturnList<OUT_LOG>("SP_GET_Lifelog_OUT_LOG_LIST", param, out spResult).ToList();

        }

        public IEnumerable<Lifelog> GetLifelogList(Lifelog data, out SP_RESULT spResult)
        {
            var param = new DynamicParameters();
            param.AddDynamicParams(new
            {
                I_CARE_RECIPIENT_ID = data.care_recipient_id,
                I_START_DATE = data.startDate,
                I_END_DATE = data.endDate,
                I_TYPE = data.searchType,
            });
            return _dapperOrm.ReturnList<Lifelog>("SP_GET_LIFELOG_LIST", param, out spResult).ToList();

        }

        
    }
}
