# Лабораторная работа №5: Разработка HTTP веб-сервера
## Описание работы
### Цели 
1.	Опробовать сетевую передачу данных по протоколу TCP/IP.
2.	Реализовать на практике события .NET.
### Задание
Написать простейший HTTP веб-сервер, способный отдавать статические файлы и, опционально, обрабатывать прочие запросы на получение данных. Проверка работоспособности веб-сервера будет выполняться через веб-браузер.

Веб-сервер должен обрабатывать только запросы на выдачу файлов (запросы GET). Если запрашиваемый файл найден, то веб-сервер должен вернуть его вместе с необходимыми заголовками HTTP.

Выполнение перечисленных выше требований оценивается как «удовлетворительно». Для получения более высоких оценок необходимо дополнительно:
-   Для получения оценки «Хорошо» в веб-сервере должно быть реализовано событие, подписываясь на которое вызывающий код сможет самостоятельно обрабатывать запрос вместо веб-сервера. Если пользовательский код обработал запрос, то серверу его обрабатывать уже не надо.
-   Для получения оценки «Отлично» размер класс веб-сервера должен быть менее 250 строк.

#### Требования к программе
Запрещено применение готовых библиотек для работы HTTP. Программа должна самостоятельно получать данные по TCP/IP, разбирать их и отдавать ответ.

Веб-сервер должен быть реализован в виде отдельного класса. Класс должен содержать только один открытый метод Start для запуска веб-сервера.  

При выполнении задания на оценку «хорошо» веб-сервер должен содержать одно событие CustomRequest, содержащее как минимум строку запроса. 

В качестве параметров (параметров конструктора или в виде свойств) веб-сервер должен получать как минимум номер TCP-порта и путь к каталогу, файлы из которого он будет отдавать.

### Демонстрация работы
1.	Запустить веб-сервер.
2.	Открыть в браузер и перейти в нём на тестовую страницу. Она должна отобразиться корректно.

## Выполнение работы
### Описание кода
#### TCPIPServer.cs

Данный класс создаёт в своём потоке сервер и начинает слушать определённый порт

Поля класса и конструктор

```C#
// Словарь MIME-типов
private Dictionary<string, string> extensions = new Dictionary<string, string>()
{ 
    {".css", "text/css"},
    {".ico", "image/x-icon"},
    {".html", "text/html"},
    {".jpg", "image/jpeg"},
    {".png", "image/png"}
};
// Отдельный поток для сервера и директория с исходными файлами
private Thread serverThread;
private string rootDirectory;
// Слушатель для клиентских запросов и номер порта
private TcpListener listener;
private int port;
// Инициализируем поля в конструкторе
public TCPIPServer(string path, int port)
{
    this.rootDirectory = path;
    this.port = port;
}
```

В классе два открытых метода
1.  Запуса сервера 
```C#
public void Start()
{
    serverThread = new Thread(this.Listen);
    serverThread.Start();
}
```

2.  Остановка сервера
```C#
public void Stop()
{
    serverThread.Abort();
    listener.Stop();
}
```

Разберём метод, который обрабатывает все клиентские запросы 

```C#
private void Listen()
{
    // Создаём нового слушателя клиентских запросов и запускаем его
    listener = new TcpListener(IPAddress.Any, port);
    listener.Start();
    while (true)
    {
        try
        {
            // Даём согласие на принятие клиенского запроса и обработаем его в методе Process
            TcpClient client = listener.AcceptTcpClient();
            Process(client);
        }
        // Выведем ошибки в консоль, если во время общения клиента/сервера что-то пойдёт не так
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
```

Метод для обработки запросов клиента

При заходе на веб страницу браузер требует от сервера файл index.html, если он есть, то браузер по чепочке запрашивает все его зависимые файлы

```C#
private void Process(TcpClient client)
{
    // Открываем поток для доступа к сети и считываем с него информацию и запросах
    NetworkStream stream = client.GetStream();
    using (StreamReader sr = new StreamReader(stream))
    {
        string file = "";
        // Считываем весь запрос
        while (true)
        {
            string line = sr.ReadLine();
            if (string.IsNullOrEmpty(line))
            {
                break;
            }
            // Нас интересует строка с методом GET, всю мета информацию скипаем
            if (line.Contains("GET"))
            {
                // Записываем название запрашиваемого файла, чтобы сервер нашел его у себя
                file = line.Split(' ')[1].Length == 1 ? "/index.html" : line.Split(' ')[1].Split('?')[0];
                Console.WriteLine("Requested " + file + "...");
            }
        }
        // Получаем ответ от сервера в виде массива байт. Функцию ResponseBytes() рассмотрим ниже
        byte[] responseBytes = ResponseBytes(rootDirectory + file); 
        // Отправляем клиенту информацию через сетевой поток
        stream.Write(responseBytes, 0, responseBytes.Length);
        stream.Flush();
        // Закрываем данный клиентский запрос
        client.Close();
    }
}
```

Описание метода ResponseBytes()

Данный метод позволяет серверу сформировать массив байт запрашиваемого клиентом файла 

```C#
private byte[] ResponseBytes(string file)
{
    // По расширению файла определяем требуемый MIME-тип
    string extension = Path.GetExtension(file);
    string contentType = extensions.ContainsKey(extension) ? extensions[extension] : "application/octet-stream";
    // Определяем длину файла
    byte[] input = File.ReadAllBytes(file);
    long contentLength = input.Length;
    // Формируем HTTP заголовок, чтобы браузер смог разобрать полученную от сервера информацию
    string header = "HTTP/1.1 200 OK\nContent-Type: " + contentType + 
        "\nContent-Length: " + contentLength + "\n\n";
    // Преобразуем HTTP заголовок в последовательность байт
    byte[] headerBytes = Encoding.ASCII.GetBytes(header);
    // Объединяем массивы байт заголовка и запрашиваемого файла
    byte[] responseBytes = new byte[headerBytes.Length + input.Length];
    Array.Copy(headerBytes, 0, responseBytes, 0, headerBytes.Length);
    Array.Copy(input, 0, responseBytes, headerBytes.Length, input.Length);
    // Возвращаем правильно сформированный массив байт
    return responseBytes;
}
```

#### Program.cs

В мейне кроме самого TCPIP сервера ещё запускается консольный поток, который позволяет из консоли выключить сервер

```C#
public static TCPIPServer server;

static void Main(string[] args)
{
    // Запускаем сервер
    server = new TCPIPServer("../../files", 322);
    server.Start();
    Console.WriteLine("Server is running on this port: " + server.Port);
    // Запускаем поток для выхода
    Thread exitThread = new Thread(Exit);
    exitThread.Start();
}

public static void Exit()
{
    while (true)
    {
        // Если в консоли ввели exit, то сервер останавливается и программа закрывается через некоторое время
        if (Console.ReadLine() == "exit")
        {
            server.Stop();
            Console.WriteLine("Server was stopped!");
            Thread.Sleep(1500);
            Environment.Exit(0);
        }
    }
}
```

### Демонстрация работы

1.  Запуск сервера 

![](resources/requests.gif)

2.  Спрячем пару файлов, чтобы посмотреть на ошибки в консоли

![](resources/errors.gif)