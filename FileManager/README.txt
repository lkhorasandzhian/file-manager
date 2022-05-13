ENG
-------------------------------------------------- ------------
-------------------|File Manager|--------------------
-------------------------------------------------- ------------
ATTENTION! Please read the instructions in full to
there were no further questions about the use.

The program is a manager capable of interacting
mess with folders and files.

Program Request Notes:

"Specify the full path to the folder" means to specify its entire di-
rector, starting from the disk to the name of the folder you are looking for.
For example, we have a C drive and an example folder, from which we
we want to interact, then we write:
C:\Folder1\Folder2\Folder3\Folder4\example

"Specify the full path to the file" means to specify its entire di-
rector, starting from the disk to the name of the desired file,
including its extension. For example, we have a D drive and
text file exampleText, which is located in
example folder and with which we want to interact,
then we write:
C:\Folder1\Folder2\Folder3\Folder4\example\exampleText.txt
When ASKING FOR THE PATH TO THE FILE, then do not forget after
name SPECIFY FILE EXTENSION.

"Enter file name" - unlike the previous paragraph
you need to enter ONLY the name of the file with
its extension. For example, for a text file named
onlyExampleText we write:
onlyExampleText.txt

For a list of commands, use the 'help' command.
I'm also copying the list here:
|-------------------------------|
|Features of the file manager|
|-------------------------------|

Additional commands:

'clean.screen' - clearing the screen from the current records.
'exit' - exit from the program.
'help' is the command you just called to get more usage information. =)

-------------------------------------------------- -------------------------------------------------- --------

Basic commands:

Each item that describes an action has a list of commands. The names of the teams were composed according to the principle 'verb.noun' in Latin.

1. view the list of computer disks and select a disk:
'get.disk_list' - view the list of disks;
'select.disk' - disk selection.

2. change to another directory (folder selection):
'select.directory' - stepwise step in the current directory down (select a subfolder from the list available in the current folder) or up (leave the current folder and go to the folder one level higher);
'select.full_path' - specify the full path to the folder.

3. view the list of files in a directory:
'view.directory_files' - view the list of files;
'view.directory_folders' - view the list of folders;
'view.current_path' - view the current full path.

4. outputting the contents of a text file to the console in UTF-8 encoding:
'print.file_content_utf8' - print file content to
UTF-8 encoding.

6. file copy:
'copy.file' - copy a file to an existing one with the same extension. The file can be overwritten.
Note: copying a file under the same name to the same directory (i.e. copying the file to itself) is not possible.

7. moving the file to a user-selected directory:
'move.file' - move the file along the full path with the possibility of renaming the file itself.
Note: The source file cannot be moved with the original name to the same directory where it was originally located.

8.deleting a file:
'delete.file' - delete a file in the specified directory.

9. creating a simple text file in UTF-8 encoding:
'create.file_utf8' - create a text file
in UTF-8 encoding.

11.Concatenate the contents of two or more text files and output the result to the console in UTF-8 encoding.
'concatenate.files' - concatenate two files and output
result to the console in UTF-8 encoding. (Multiple files
can be concatenated by calling the appropriate command
as many times as needed.

I apologize for such a detailed guide, but
all these efforts have been spent for your own convenience!
Thank you for your attention! :)



RUS
--------------------------------------------------------------
-------------------|Файловый менеджер|--------------------
--------------------------------------------------------------
ВНИМАНИЕ! Прошу прочитать инструкцию полностью, дабы
в дальнейшем не возникало вопросов по использованию.

Программа является менеджером, способным взаимодейст-
вовать с папками и файлами.

Примечания к просьбам программы:

"Укажите полный путь к папке" означает указать всю его ди-
ректорию, начиная от диска до наименования искомой папки.
Например, у нас есть диск C и папка example, с которой мы
хотим взаимодействовать, тогда пишем:
C:\Folder1\Folder2\Folder3\Folder4\example

"Укажите полный путь к файлу" означает указать всю его ди-
ректорию, начиная от диска до именования искомого файла,
включая его расширение. Например, у нас есть диск D и 
текстовый файл exampleText, который находится в
папке example и с которым мы хотим взаимодействовать,
тогда пишем:
C:\Folder1\Folder2\Folder3\Folder4\example\exampleText.txt
Когда ПРОСЯТ ПУТЬ К ФАЙЛУ, то не забываем после
названия УКАЗЫВАТЬ РАСШИРЕНИЕ ФАЙЛА.

"Введите название файла" - в отличие от прошлого пункта
необходимо ввести ТОЛЬКО само наименование файла с
его расширением. Например, для текстового файла с именем
onlyExampleText пишем:
onlyExampleText.txt

За списком команд обращайтесь по команде 'help'.
Также копирую перечень сюда:
|-------------------------------|
|Возможности файлового менеджера|
|-------------------------------|

Дополнительные команды:

'clean.screen' - очистка экрана от текущий записей.
'exit' - завершение работы с программой.
'help' - команда, которую Вы вызвали только что, чтобы получить дополнительные сведения по использованию. =)

------------------------------------------------------------------------------------------------------------

Основные команды:

К каждому пункту, описывающему действие, приведён список команд. Наименования команд были составлены по принципу 'глагол.существительное' на латинице.

1. просмотр списка дисков компьютера и выбор диска:
'get.disk_list' - просмотр списка дисков;
'select.disk' - выбор диска.

2. переход в другую директорию (выбор папки):
'select.directory' - ступенчатый шаг в текущей директории вниз (выбор подпапки из списка доступных в текущей папке) или вверх (выход из текущей папки и переход в папку на один уровень выше);
'select.full_path' - указать полный путь к папке.

3. просмотр списка файлов в директории:
'view.directory_files' - просмотр списка файлов;
'view.directory_folders' - просмотр списка папок;
'view.current_path' - просмотр текущего полного пути.

4. вывод содержимого текстового файла в консоль в кодировке UTF-8:
'print.file_content_utf8' - вывод содержимого файла в
кодировке UTF-8.

6. копирование файла:
'copy.file' - копирование файла в существующий с тем же расширением. Возможна перезапись файла.
Примечание: копирование файла под тем же именем в ту же директорию(т.е.копирование файла самого в себя) невозможно.

7. перемещение файла в выбранную пользователем директорию:
'move.file' - перемещение файла по полному пути с возможностью переименовывания самого файла.
Примечание: исходный файл не может быть перемещён с исходным именем в ту же директорию, в которой он находился изначально.

8. удаление файла:
'delete.file' - удаление файла по указанной директории.

9. создание простого текстового файла в кодировке UTF-8:
'create.file_utf8' - создание текстового файла
в кодировке UTF-8.

11.конкатенация содержимого двух или более текстовых файлов и вывод результата в консоль в кодировке UTF-8.
'concatenate.files' - конкатенация двух файлов и вывод
результата в консоль в кодировке UTF-8. (Несколько файлов
можно конкатенировать вызовом соотвествующей команды
тем числом раз, которое будет необходимо.

Прошу прощения за столь подробное руководство, но
все эти старания были потрачены для Вашего же удобства!
Спасибо за внимание! :)