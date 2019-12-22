## Курсовая работа по БСБД(Безопасность систем баз данных)

Вариант курсовой работы №17 (Кондитерская)

### Версии:

+ API разработан на языке C# c использованием WCF(Windows Communication Foundation)
+ Серевер базы данных MariaDB

### Er - Диаграмма:

![Image alt](https://github.com/kiryosha/DS-course-work/raw/master/er/er.jpg)

### Как начать 

+ Клонировать репозиторий

		git clone https://github.com/kiryosha/DS-course-work.git

+ Скачайте [xampp](https://www.apachefriends.org/ru/index.html)
+ Установите и запустить
+ Создайте бд confectionerydb
+ Импортируйте базу, для этого нужно нажать **импорт** в phpmyadmin
+ Перейдите во вкладку **привелегии** создайте пользователя

		username = user_db
		password = q1234q
		Глобальные привилегии: SELECT, INSERT, UPDATE, DELETE
 
+ Для запуска приложение нужно запустить *Host.exe* (Host/bin/Debug/Host.exe), затем *Client.exe* (Client/bin/Debug/Client.exe)

[Отчет о проделанной работe](https://github.com/kiryosha/DS-course-work/blob/master/%D0%9E%D1%82%D1%87%D0%B5%D1%82.docx?raw=true)