using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temprature_controller
{
    class temp_control : temp_dtls
    {
        int max = 70, min = 40;
        public delegate void temp_delegate(int val);
        public event temp_delegate temp_event;
        public temp_control()
        {
            this.temp_event += alarm;
            this.temp_event += insert;
        }
        void check_temp()
        {
            bool Flag = true;
            while (Flag == true)
            {
                Console.WriteLine("Select the option");
                Console.WriteLine("1. Check_temp 2. Print queries 3.Exit\n");
                int option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        generate_num();
                        break;
                    case 2:
                        print_data();
                        break;
                    case 3:
                        Flag = false;
                        break;
                }
            }
        }
        void alarm(int temp)
        {
            Console.WriteLine("\ntemprature out of bounds\n");
        }
        void generate_num()
        {
            Random random = new Random();
            int ran = random.Next(min - 30, max + 30);
            Console.WriteLine("current temprature is " + ran);
            if (ran > max || ran < min)
            {
                temp_event(ran);
            }
        }
        public static void Main(string[] args)
        {
            temp_control check = new temp_control();
            check.check_temp();
        }
        void insert(int temp)
        {
            using (Room_tempEntities temp_obj = new Room_tempEntities())
            {
                temp_dtls room_temp = new temp_dtls
                {
                    Temprature = temp,
                    currnt_time = DateTime.Now,
                };
                temp_obj.temp_dtls.Add(room_temp);
                temp_obj.SaveChanges();
            }
        }
        void print_data()
        {
            Room_tempEntities temp_obj = new Room_tempEntities();
            temp_dtls room_temp = new temp_dtls();
            var filteredResult = from s in temp_obj.temp_dtls
                                 where s.Temprature > max
                                 select s;
            foreach (var item in filteredResult)
            {
                Console.WriteLine("when temprature was increased");
                Console.WriteLine(item.Temprature);
                Console.WriteLine(item.currnt_time);
            }
            var filteredResult2 = from s in temp_obj.temp_dtls
                                  where s.Temprature < min
                                  select s;
            foreach (var item in filteredResult2)
            {
                Console.WriteLine("when temprature was decreased");
                Console.WriteLine(item.Temprature);
                Console.WriteLine(item.currnt_time);
            }
        }
    }
}
