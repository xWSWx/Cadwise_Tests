# Тестовое задание Cadwise
### Как сбилдить
1. "dotnet build" в корневом каталоге
1. либо открыть через IDE VS2022
### Общие допущения
Сознательно не использовано:
1. Темы WPF
1. Локализация WPF (CurrentUICulture)
1. Ресурсы WPF (вернее, в Обработчике файлов есть несколько ресурсов в Resources.resx, просто как намёк что, "я помню, всё ок")
1. Не реализован "словарь цветов", равно как и "превью всех цветов"
1. Не реализована песочница для просмотра стилей
1. Папка для итоговых файлов - **стандартная** ({project}\bin\Debug\net6.0-windows\)
## Обработчик файлов
![Example](https://github.com/xWSWx/Cadwise_Tests/assets/29701338/15050231-a6b1-4a7a-8cc1-b284cff92fd3)
### Как пользоваться
1. Нажать на "плюсик"
1. Выбрать файлы на обработку
1. Выбрать вариант обработки
1. Запустить обработку
1. *управлять обработкой
1. Повторять предыдущие пункты любое количество раз.
1. **Запустить 20 файлов по 20гб и посмотреть нагрузку на процессор/оперативную память
1. Каждый обработанный файл помечается "галочкой"
1. Отмена - отменяет весь процесс и удаляет файлы.
1. Pause/Resume на одну и ту же кнопку.   
### Допущения
1. Чтобы вручную не вбивать для каждого файлы выходной файл, просто введено понятие "prefix"
1. Prefix не валидируется на предмет символов, запрещённых в имени файла.. сорри. надо будет - сделаю валидацию
## Эмулятор терминала Desert Bank Terminal
![Example](https://github.com/xWSWx/Cadwise_Tests/assets/29701338/f2d31ed0-ab35-4b90-8967-2997544cfd9c)
### Допущения
1. Банк хранения банкнот, по хорошему, должен представлять из себя управленца списка эмуляторов контроллеров купюрохранилищ.. Но, в итоге, представляет собой лишь Dictianary<int,int>(номинал, количество)
1. Наличные у пользователя - бесконечные
1. Пинкод написан прямо на карточке
1. Чтобы не насиловать консоль, состояние терминала (тот самый Dictionary<int,int>) можно смотреть с помощью "админской карточки"
1. Отсутствие визуализации "Картоприёмник_Занят"
1. В боковые кнопочки не прокинуты команды. Пока они просто для "красоты"
## Генератор файлов
Сделанный за 5 минут на коленке файл-генератор
### Как пользоваться
LargeFileGenerator.exe <file_path> <file_size_in_bytes> 
Правда, где то ошибся и file_size_in_bytes игнорируется.. ну и чёрт с ним. нажимаем в консоли ctrl+c когда надоест и всё в порядке.
Может.. потом поправлю.
