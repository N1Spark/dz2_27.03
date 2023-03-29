using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz2_27._03
{
    class Program
    {
        public class FasadeComputer
        {
            protected VideoCard videoCard;
            protected RAM ram;
            protected HDD hdd;
            protected ODD odd;
            protected PSU psu;
            protected Sensor sensor;
            public FasadeComputer(VideoCard videoCard, RAM ram, HDD hdd, ODD odd, PSU psu, Sensor sensor)
            {
                this.videoCard = videoCard;
                this.ram = ram;
                this.hdd = hdd;
                this.odd = odd;
                this.psu = psu;
                this.sensor = sensor;
            }
            public void Start()
            {
                while (true)
                {
                    if (!psu.ElectricitySupply())
                    {
                        Console.WriteLine("Произошла ошибка в блоке питания");
                        break;
                    }
                    Console.WriteLine("Происходит подача напряжения");

                    if (!sensor.VoltageTest(true))
                    {
                        Console.WriteLine("Напряжение нестабильное");
                        break;
                    }
                    Console.WriteLine("Напряжение в норме");

                    if (!sensor.CheckPowerSupplyTemp(psu))
                    {
                        Console.WriteLine("Температура блока питания нестабильная");
                        break;
                    }
                    Console.WriteLine("Температура блока питания в норме");

                    if (!sensor.CheckVideoCardTemp(videoCard))
                    {
                        Console.WriteLine("Температура видеокарты нестабильная");
                        break;
                    }
                    Console.WriteLine("Температура видеокарты в норме");

                    if (!psu.CheckPowerSupplyToVideoCard())
                    {
                        Console.WriteLine("Произошла ошибка в блоке питания");
                        break;
                    }
                    Console.WriteLine("Происходит подача питания в видеокарту");

                    if (!videoCard.VideoCardLaunch())
                    {
                        Console.WriteLine("Произошла ошибка в видеокарте");
                        break;
                    }
                    Console.WriteLine("Происходит запуск видеокарты");

                    if (!videoCard.MonitorCheck())
                    {
                        Console.WriteLine("Произошла ошибка в видеокарте, нет доступа к монитору");
                        break;
                    }
                    Console.WriteLine("Происходит связь с монитором");

                    if (!sensor.CheckRAM(ram))
                    {
                        Console.WriteLine("Температура оперативной памяти нестабильная");
                        break;
                    }
                    Console.WriteLine("Температура оперативной памяти в норме");

                    if (!psu.PowerSupplyRAM())
                    {
                        Console.WriteLine("Произошла ошибка в блоке питания");
                        break;
                    }
                    Console.WriteLine("Происходит подача питания в оперативную память");

                    if (!ram.LaunchDevices())
                    {
                        Console.WriteLine("Ошибка в оперативной памяти");
                        break;
                    }
                    Console.WriteLine("Происходит запуск устройств");

                    if (!ram.DataAnalysis())
                    {
                        Console.WriteLine("Ошибка в оперативной памяти");
                        break;
                    }
                    Console.WriteLine("Происходит анализ памяти");

                    Console.WriteLine($"Информация про оперативную память:\n{videoCard.ShowRAM(ram)}");

                    if (!psu.SupplyingPowerODR())
                    {
                        Console.WriteLine("Произошла ошибка в блоке питания");
                        break;
                    }
                    Console.WriteLine("Подача питания на устройство для чтения дисков");

                    if (!odd.StartingOpticalDiscReader())
                    {
                        Console.WriteLine("Ошибка запуска чтения оптических дисков");
                        break;
                    }
                    Console.WriteLine("Запуск устройства чтения оптических дисков");

                    if (!odd.CheckingDiskPresence(true))
                    {
                        Console.WriteLine("Ошибка! Отсутствует оптический диск");
                        break;
                    }
                    Console.WriteLine("Наличие оптического диска");

                    Console.WriteLine($"Информация про оптические диски:\n{videoCard.ShowODD(odd)}");

                    if (!psu.PowerHDD())
                    {
                        Console.WriteLine("Произошла ошибка в блоке питания");
                        break;
                    }
                    Console.WriteLine("Питание подается на жесткий диск");

                    if (!hdd.HDDStartup(true))
                    {
                        Console.WriteLine("Ошибка! запуска жесткого диска");
                    }
                    Console.WriteLine("Запуск жесткого диска");

                    if (!hdd.BootDiskCheck(true))
                    {
                        Console.WriteLine("Загрузочный диск поврежден");
                    }
                    Console.WriteLine("Загрузочный диск в норме");

                    Console.WriteLine($"Информация про жесткий диск:\n{videoCard.ShowHDD(hdd)}");

                    break;
                }
            }
            public void Shutdown()
            {
                while (true)
                {
                    if (!hdd.DeviceShutdown())
                    {
                        Console.WriteLine("Ошибка!");
                        break;
                    }
                    Console.WriteLine("Остановка жесткого диска");

                    if (!ram.ClearingMemory())
                    {
                        Console.WriteLine("Oшибка очистки памяти");
                        break;
                    }
                    Console.WriteLine("Очистка оперативной памяти");

                    if (!odd.StartingPosition())
                    {
                        Console.WriteLine("Ошибка! Оптических дисков");
                        break;
                    }
                    Console.WriteLine("Возврат оптических дисков");

                    if (!psu.PowerOutage())
                    {
                        Console.WriteLine("Ошибка! Блока питания");
                        break;
                    }
                    Console.WriteLine("Прекращение питания всех устройств");

                    if (sensor.VoltageTest(false))
                    {
                        Console.WriteLine("Ошибка!");
                    }
                    Console.WriteLine("Напряжения нет");
                    Console.WriteLine("Выключение блока питания");
                    break;

                }
            }
        }
        public class VideoCard
        {
            public double Temperature { get; set; }
            public VideoCard(double temperature)
            {
                Temperature = temperature;
            }
            public bool VideoCardLaunch()
            {
                return true;
            }
            public bool MonitorCheck()
            {
                return true;
            }
            public string ShowRAM(RAM ram)
            {
                return $"^Объем памяти: {ram.Memory}\n^Тип памяти: {ram.TypeRAM}\n^Использовано памяти: {ram.MemoryUsage}\n^Температура: {ram.Temperature}";
            }
            public string ShowODD(ODD odd)
            {
                return $"^Тип диска {odd.TypeODD} - {odd.UseROM}";
            }
            public string ShowHDD(HDD hdd)
            {
                return $"Объем жесткого диска: {hdd.Capacity}\nРазмер буфера: {hdd.BufferSize}";
            }
        }
        public class RAM
        {
            public double Temperature { get; set; }
            public string TypeRAM { get; set; }
            public int Memory { get; set; }
            public double MemoryUsage { get; set; }
            public RAM(double temperature, string typeRAM, int memory, double memoryUsage)
            {
                Temperature = temperature;
                TypeRAM = typeRAM;
                Memory = memory;
                MemoryUsage = memoryUsage;
            }
            public bool LaunchDevices()
            {
                return true;
            }
            public bool DataAnalysis()
            {
                return true;
            }
            public bool ClearingMemory()
            {
                MemoryUsage = 0;

                return true;
            }

        }
        public class HDD
        {
            public string Capacity { get; set; }
            public int BufferSize { get; set; }
            public HDD(string capacity, int bufferSize)
            {
                Capacity = capacity;
                BufferSize = bufferSize;
            }

            public bool HDDStartup(bool test)
            {
                if (test)
                    return true;
                return false;
            }
            public bool BootDiskCheck(bool test)
            {
                if (test)
                    return true;
                return false;
            }

            public bool DeviceShutdown()
            {
                return true;
            }
        }
        public class ODD
        {
            public string TypeODD { get; set; }
            public string UseROM { get; set; }

            public ODD(string typeODD, string useROM)
            {
                TypeODD = typeODD;
                UseROM = useROM;
            }

            public bool StartingOpticalDiscReader()
            {
                return true;
            }
            public bool CheckingDiskPresence(bool test)
            {
                if (test)
                    return true;
                return false;
            }
            public bool StartingPosition()
            {
                return true;
            }
        }
        public class PSU
        {
            public double Temperature { get; set; }
            public PSU(double temperature)
            {
                Temperature = temperature;
            }

            public bool ElectricitySupply()
            {
                return true;
            }
            public bool CheckPowerSupplyToVideoCard()
            {
                return true;
            }
            public bool PowerSupplyRAM()
            {
                return true;
            }
            public bool SupplyingPowerODR()
            {
                return true;
            }
            public bool PowerHDD()
            {
                return true;
            }

            public bool PowerOutage()
            {
                return true;
            }
        }
        public class Sensor
        {
            public bool VoltageTest(bool test)
            {
                if (test)
                    return true;
                return false;
            }
            public bool CheckPowerSupplyTemp(PSU psu)
            {
                if (psu.Temperature > 50 || psu.Temperature < 30)
                    return false;
                return true;
            }
            public bool CheckVideoCardTemp(VideoCard videoCard)
            {
                if (videoCard.Temperature > 80 || videoCard.Temperature < 60)
                    return false;
                return true;
            }
            public bool CheckRAM(RAM ram)
            {
                if (ram.Temperature > 45 || ram.Temperature < 25)
                    return false;
                return true;
            }
        }
        static void Main(string[] args)
        {
            RAM ram = new RAM(30, "DDR4", 16, 15);
            VideoCard videoCard = new VideoCard(70);
            HDD hDD = new HDD("5 ТB", 500);
            ODD oDD = new ODD("DVD", "DVD-ROM");
            PSU psu = new PSU(50);
            Sensor sensor = new Sensor();
            FasadeComputer comp = new FasadeComputer(videoCard, ram, hDD, oDD, psu, sensor);
            comp.Start();
            comp.Shutdown();
        }
    }
}
