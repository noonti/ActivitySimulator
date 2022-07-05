using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHandler.Model
{
    public class Lifelog
    {
        public int id { get; set; }
        public int care_recipient_id { get; set; }
        public int lifelog_date { get; set; } // lifelog 날짜
        public byte[] lifelog_date_1_list = new byte[4 * 60 * 24]; //AIX_1이 저장된 날짜 목록(4*60*24=5760byte)
        public byte[] validation_1_list = new byte[1 * 60 * 24]; // AIX_1의 유효 여부 목록(1*60*24=1440byte) 0xFF : 유효하지 않음
        public byte[] place_code_1_list = new byte[1 * 60 * 24]; // place_1 목록(1*60*24=1440byte)
        public byte[] AIX_1_list = new byte[2 * 60 * 24];      // AIX_1 목록(2*60*24=2880byte)
        public byte[] lifelog_date_h_list = new byte[4 * 24]; // AIX_h이 저장된 날짜 목록(4*24=96byte)',
        public byte[] validation_h_list = new byte[1 * 24]; // AIX_h의 유효 여부 목록(1*24=24byte) 0xFF : 유효하지 않음',
        public byte[] AIX_h_list = new byte[2 * 24]; //AIX_h 목록(2*24=48byte)',
        public byte[] place_code_h_p1_list = new byte[1 * 24]; //AIX_h_p1_list의 place code 목록(1*24=24byte)',
        public byte[] place_code_h_p2_list = new byte[1 * 24]; //AIX_h_p2_list의 place code 목록(1*24=24byte)',
        public byte[] place_code_h_p3_list = new byte[1 * 24]; //AIX_h_p3_list의 place code 목록(1*24=24byte)',
        public byte[] place_code_h_p4_list = new byte[1 * 24]; //AIX_h_p4_list의 place code 목록(1*24=24byte)',
        public byte[] place_code_h_p5_list = new byte[1 * 24]; //AIX_h_p5_list의 place code 목록(1*24=24byte)',
        public byte[] place_code_h_p6_list = new byte[1 * 24]; //AIX_h_p6_list의 place code 목록(1*24=24byte)',
        public byte[] place_code_h_p7_list = new byte[1 * 24]; //AIX_h_p7_list의 place code 목록(1*24=24byte)',
        public byte[] place_code_h_p8_list = new byte[1 * 24]; //AIX_h_p8_list의 place code 목록(1*24=24byte)',
        public byte[] place_code_h_p9_list = new byte[1 * 24]; //AIX_h_p9_list의 place code 목록(1*24=24byte)',
        public byte[] place_code_h_p10_list = new byte[1 * 24]; //AIX_h_p10_list의 place code 목록(1*24=24byte)',
        public byte[] AIX_h_p1_list = new byte[2 * 24]; //place 1의 AIX_h 목록(2*24=48byte)',
        public byte[] AIX_h_p2_list = new byte[2 * 24]; //place 2의 AIX_h 목록(2*24=48byte)',
        public byte[] AIX_h_p3_list = new byte[2 * 24]; //place 3의 AIX_h 목록(2*24=48byte)',
        public byte[] AIX_h_p4_list = new byte[2 * 24]; //place 4의 AIX_h 목록(2*24=48byte)',
        public byte[] AIX_h_p5_list = new byte[2 * 24]; //place 5의 AIX_h 목록(2*24=48byte)',
        public byte[] AIX_h_p6_list = new byte[2 * 24]; //place 6의 AIX_h 목록(2*24=48byte)',
        public byte[] AIX_h_p7_list = new byte[2 * 24]; //place 7의 AIX_h 목록(2*24=48byte)',
        public byte[] AIX_h_p8_list = new byte[2 * 24]; //place 8의 AIX_h 목록(2*24=48byte)',
        public byte[] AIX_h_p9_list = new byte[2 * 24]; //place 9의 AIX_h 목록(2*24=48byte)',
        public byte[] AIX_h_p10_list = new byte[2 * 24]; //place 10의 AIX_h 목록(2*24=48byte)',
        public int lifelog_date_d { get; set; } // AIX_d이 저장된 날짜',
        public byte[] validation_d = new byte[1];//AIX_d의 유효 여부. 0xFF : 유효하지 않음',
        public int AIX_d { get; set; } //AIX_d',
        public byte[] place_code_d_p1 = new byte[1];//AIX_d_p1의 place code',
        public byte[] place_code_d_p2 = new byte[1];//AIX_d_p2의 place code',
        public byte[] place_code_d_p3 = new byte[1];//AIX_d_p3의 place code',
        public byte[] place_code_d_p4 = new byte[1];//AIX_d_p4의 place code',
        public byte[] place_code_d_p5 = new byte[1];//AIX_d_p5의 place code',
        public byte[] place_code_d_p6 = new byte[1];//AIX_d_p6의 place code',
        public byte[] place_code_d_p7 = new byte[1];//AIX_d_p7의 place code',
        public byte[] place_code_d_p8 = new byte[1];//AIX_d_p8의 place code',
        public byte[] place_code_d_p9 = new byte[1];//AIX_d_p9의 place code',
        public byte[] place_code_d_p10 = new byte[1];//AIX_d_p10의 place code',
        public int AIX_d_p1 { get; set; } // place 1의 AIX_d',
        public int AIX_d_p2 { get; set; } // place 2의 AIX_d',
        public int AIX_d_p3 { get; set; } // place 3의 AIX_d',
        public int AIX_d_p4 { get; set; } // place 4의 AIX_d',
        public int AIX_d_p5 { get; set; } // place 5의 AIX_d',
        public int AIX_d_p6 { get; set; } // place 6의 AIX_d',
        public int AIX_d_p7 { get; set; } // place 7의 AIX_d',
        public int AIX_d_p8 { get; set; } // place 8의 AIX_d',
        public int AIX_d_p9 { get; set; } // place 9의 AIX_d',
        public int AIX_d_p10 { get; set; } // place 10의 AIX_d',
        public byte[] sleep_depth_1_list = new byte[1 * 60 * 24];//sleetDepth_1 목록(1*60*24=1440byte)',
        public int sleep_start_time_d { get; set; } // 수면시작시간(전일 21시 부터의 분)',
        public int sleep_end_time_d { get; set; } //수면종료(wakeup)시간(전일 21시 부터의 분)',
        public byte[] bath_count_d_list = new byte[4 * 2];//해당일의 화장실 방문 횟수(4*2=8byte)',
        public byte[] bath_time_d_list = new byte[4 * 2];//해당일의 화장실 방문 시간(4*2=8byte)',
        public byte[] outgoing_1_list = new byte[1 * 60 * 24];//outgoing_1 목록(1*60*24=1440byte)',
        public byte[] outgoing_count_d_list = new byte[4 * 2];//해당일의 외출 횟수(4*2=8byte)',
        public byte[] outgoing_time_d_list = new byte[4 * 2];//해당일의 외출 시간(4*2=8byte)',
        public byte[] outgoing_late_night_count_d_list = new byte[4 * 2];//해당일의 심야 회출 횟수(4*2=8byte)',
        public byte[] outgoing_late_night_time_d_list = new byte[4 * 2];//해당일의 심야 회출 시간(4*2=8byte)',
        public byte[] total_AIX_h_list = new byte[4 * 24];//해당일까지의 AIX_h total sum 시간대별 목록(4*24=96byte)',
        public byte[] total_AIX_h_count_list = new byte[4 * 24];//해당일까지의 AIX_h total count 시간대별 목록(4*24=96byte)',
        public int total_AIX_d {get;set;} // 해당일까지의 AIX_d total sum',
        public int total_AIX_d_count { get; set; } // 해당일까지의 AIX_d total count',
        public int total_sleep_start_time_d { get; set; } // 해당일까지의 수면시작시간(전일 21시 부터의 분) total sum',
        public int total_sleep_start_time_d_count { get; set; } // 해당일까지의 수면시작시간 산정 날짜(일) count',
        public int total_sleep_end_time_d { get; set; } // 해당일까지의 수면종료(wakeup)시간(전일 21시 부터의 분) total sum',
        public int total_sleep_end_time_d_count { get; set; } // 해당일까지의 수면종료(wakeup)시간 산정 날짜(일) count',
        public int total_bath_count_d { get; set; } // 해당일까지의 화장실 횟수 total sum',
        public int total_bath_count_d_count { get; set; } // 해당일까지의 화장실 횟수 total 날짜(일) count',
        public int total_bath_time_d { get; set; } // 해당일까지의 화장실 이용 시간 total sum',
        public int total_bath_time_d_count { get; set; } // 해당일까지의 화장실 이용 시간 total 날짜(일) count',
        public int total_outgoing_count_d { get; set; } // 해당일까지의 외출 횟수 total sum',
        public int total_outgoing_count_d_count { get; set; } // 해당일까지의 외출 횟수 total 날짜(일) count',
        public int total_outgoing_time_d { get; set; } // 해당일까지의 외출 시간 total sum',
        public int total_outgoing_time_d_count { get; set; } // 해당일까지의 외출 시간 total 날짜(일) count',
        public String updated_at { get; set; }
        public String created_at { get; set; }

        public int startDate { get; set; }
        public int endDate { get; set; }

        public int searchType { get; set; }

    }
}
