
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHandler.Model
{
    public class LifeLog48
    {
        public int care_recipient_id { get; set; }
        public byte[] lifelog_date_1_list { get; set; }
        public byte[] validation_1_list { get; set; }
        public byte[] place_1_list { get; set; }
        public byte[] AIX_1_list { get; set; }
        public byte[] lifelog_date_h_list { get; set; }
        public byte[] validation_h_list { get; set; }
        public byte[] AIX_h_list { get; set; }
        public byte[] place_code_h_p1_list { get; set; }
        public byte[] place_code_h_p2_list { get; set; }
        public byte[] place_code_h_p3_list { get; set; }
        public byte[] place_code_h_p4_list { get; set; }
        public byte[] place_code_h_p5_list { get; set; }
        public byte[] place_code_h_p6_list { get; set; }
        public byte[] place_code_h_p7_list { get; set; }
        public byte[] place_code_h_p8_list { get; set; }
        public byte[] place_code_h_p9_list { get; set; }
        public byte[] place_code_h_p10_list { get; set; }
        public byte[] AIX_h_p1_list { get; set; }
        public byte[] AIX_h_p2_list { get; set; }
        public byte[] AIX_h_p3_list { get; set; }
        public byte[] AIX_h_p4_list { get; set; }
        public byte[] AIX_h_p5_list { get; set; }
        public byte[] AIX_h_p6_list { get; set; }
        public byte[] AIX_h_p7_list { get; set; }
        public byte[] AIX_h_p8_list { get; set; }
        public byte[] AIX_h_p9_list { get; set; }
        public byte[] AIX_h_p10_list { get; set; }
        public byte[] lifelog_date_d_list { get; set; }
        public byte[] validation_d_list { get; set; }
        public byte[] AIX_d_list { get; set; }
        public byte[] place_code_d_p1_list { get; set; }
        public byte[] place_code_d_p2_list { get; set; }
        public byte[] place_code_d_p3_list { get; set; }
        public byte[] place_code_d_p4_list { get; set; }
        public byte[] place_code_d_p5_list { get; set; }
        public byte[] place_code_d_p6_list { get; set; }
        public byte[] place_code_d_p7_list { get; set; }
        public byte[] place_code_d_p8_list { get; set; }
        public byte[] place_code_d_p9_list { get; set; }
        public byte[] place_code_d_p10_list { get; set; }
        public byte[] AIX_d_p1_list { get; set; }
        public byte[] AIX_d_p2_list { get; set; }
        public byte[] AIX_d_p3_list { get; set; }
        public byte[] AIX_d_p4_list { get; set; }
        public byte[] AIX_d_p5_list { get; set; }
        public byte[] AIX_d_p6_list { get; set; }
        public byte[] AIX_d_p7_list { get; set; }
        public byte[] AIX_d_p8_list { get; set; }
        public byte[] AIX_d_p9_list { get; set; }
        public byte[] AIX_d_p10_list { get; set; }
        public int AIX_1Eq0RepeatCount { get; set; }
        public int AIX_1Gt0RepeatCount { get; set; }
        public byte[] sleep_depth_1_list { get; set; }
        public byte[] outgoing_1_list { get; set; }
        public byte[] total_AIX_h_list { get; set; }
        public byte[] total_AIX_h_count_list { get; set; }
        public byte[] total_AIX_d_list { get; set; }
        public byte[] total_AIX_d_count_List { get; set; }
        public byte[] total_sleep_start_time_d_list { get; set; }
        public byte[] total_sleep_start_time_d_count_list { get; set; }
        public byte[] total_sleep_end_time_d_list { get; set; }
        public byte[] total_sleep_end_time_d_count_list { get; set; }
        public byte[] total_bath_count_d_list { get; set; }
        public byte[] total_bath_count_d_count_list { get; set; }
        public byte[] total_bath_time_d_list { get; set; }
        public byte[] total_bath_time_d_count_list { get; set; }
        public byte[] total_outgoing_count_d_list { get; set; }
        public byte[] total_outgoing_count_d_count_list { get; set; }
        public byte[] total_outgoing_time_d_list { get; set; }
        public byte[] total_outgoing_time_d_count_list { get; set; }

        public LifeLog48()
        {
            
            lifelog_date_1_list = new byte[11520];  // 4 * 60 * 48 
            validation_1_list = new byte[2880];     // 1 * 60 * 48
            place_1_list = new byte[2880];          // 1 * 60 * 48
            AIX_1_list = new byte[5760];            // 2*60*48=5760
            lifelog_date_h_list = new byte[192];    // 4*48=192
            validation_h_list = new byte[48];       // 1*48=48
            AIX_h_list = new byte[96];              // 2*48=96
            place_code_h_p1_list = new byte[48];    // 1*48=48
            place_code_h_p2_list = new byte[48];    // 1*48=48        
            place_code_h_p3_list = new byte[48];    // 1*48=48
            place_code_h_p4_list = new byte[48];    // 1*48=48
            place_code_h_p5_list = new byte[48];    // 1*48=48
            place_code_h_p6_list = new byte[48];    // 1*48=48
            place_code_h_p7_list = new byte[48];    // 1*48=48
            place_code_h_p8_list = new byte[48];    // 1*48=48
            place_code_h_p9_list = new byte[48];    // 1*48=48
            place_code_h_p10_list = new byte[48];   // 1*48=48
            AIX_h_p1_list = new byte[96];           // 2*48=96
            AIX_h_p2_list = new byte[96];           // 2*48=96
            AIX_h_p3_list = new byte[96];           // 2*48=96
            AIX_h_p4_list = new byte[96];           // 2*48=96
            AIX_h_p5_list = new byte[96];           // 2*48=96
            AIX_h_p6_list = new byte[96];           // 2*48=96
            AIX_h_p7_list = new byte[96];           // 2*48=96
            AIX_h_p8_list = new byte[96];           // 2*48=96
            AIX_h_p9_list = new byte[96];           // 2*48=96
            AIX_h_p10_list = new byte[96];          // 2*48=96
            lifelog_date_d_list = new byte[8];      // 4*2=8
            validation_d_list = new byte[2];        // 1*2=2
            AIX_d_list = new byte[4];               // 2*2=4
            place_code_d_p1_list = new byte[2];      // 1 * 2 = 2
            place_code_d_p2_list = new byte[2];      // 1 * 2 = 2
            place_code_d_p3_list = new byte[2];      // 1 * 2 = 2
            place_code_d_p4_list = new byte[2];      // 1 * 2 = 2
            place_code_d_p5_list = new byte[2];      // 1 * 2 = 2
            place_code_d_p6_list = new byte[2];      // 1 * 2 = 2
            place_code_d_p7_list = new byte[2];      // 1 * 2 = 2
            place_code_d_p8_list = new byte[2];      // 1 * 2 = 2
            place_code_d_p9_list = new byte[2];      // 1 * 2 = 2
            place_code_d_p10_list = new byte[2];      // 1 * 2 = 2
            AIX_d_p1_list = new byte[4];               // 2*2=4
            AIX_d_p2_list = new byte[4];               // 2*2=4
            AIX_d_p3_list = new byte[4];               // 2*2=4
            AIX_d_p4_list = new byte[4];               // 2*2=4
            AIX_d_p5_list = new byte[4];               // 2*2=4
            AIX_d_p6_list = new byte[4];               // 2*2=4
            AIX_d_p7_list = new byte[4];               // 2*2=4
            AIX_d_p8_list = new byte[4];               // 2*2=4
            AIX_d_p9_list = new byte[4];               // 2*2=4
            AIX_d_p10_list = new byte[4];               // 2*2=4

            sleep_depth_1_list = new byte[2880];            //1*60*48=2880
            outgoing_1_list = new byte[2880];             //1*60*48=2880
            total_AIX_h_list = new byte[192];                 //4*48=192
            total_AIX_h_count_list = new byte[192];                 //4*48=192

            total_AIX_d_list = new byte[8];        // 4*2=8
            total_AIX_d_count_List = new byte[8];        // 4*2=8
            total_sleep_start_time_d_list = new byte[8];        // 4*2=8
            total_sleep_start_time_d_count_list = new byte[8];        // 4*2=8
            total_sleep_end_time_d_list = new byte[8];        // 4*2=8
            total_sleep_end_time_d_count_list = new byte[8];        // 4*2=8
            total_bath_count_d_list = new byte[8];        // 4*2=8
            total_bath_count_d_count_list = new byte[8];        // 4*2=8
            total_bath_time_d_list = new byte[8];        // 4*2=8
            total_bath_time_d_count_list = new byte[8];        // 4*2=8
            total_outgoing_count_d_list = new byte[8];        // 4*2=8
            total_outgoing_count_d_count_list = new byte[8];        // 4*2=8
            total_outgoing_time_d_list = new byte[8];        // 4*2=8
            total_outgoing_time_d_count_list = new byte[8];        // 4*2=8

        }
    }
}
