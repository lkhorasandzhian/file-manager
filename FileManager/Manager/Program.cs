#pragma warning disable

using System;
using System.IO;
using System.Threading;

namespace Manager
{
    /// <summary>
    /// Основной класс программы.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Текущая отображаемая директория для перемещения.
        /// </summary>
        public static string CurrentFullPath = Path.GetFullPath(@"..\..\..\.");

        /// <summary>
        /// Точка входа программы. Здесь принимается ввод команды в консоли, после чего она передаётся
        /// в один из case'ов switch'а.
        /// </summary>
        private static void Main()
        {
            try
            {
                Console.WriteLine("Добро пожаловать! Вы используете приложение \"Файловый Менеджер\"!");
                Console.WriteLine("Для ознакомления с полным списком доступных команд и правилами пользования " +
                                  "файловым менеджером введите команду 'help'");
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine(Environment.NewLine + CurrentFullPath);
                bool exit = false;

                string command;
                do
                {
                    bool command_checker;
                    do
                    {
                        command_checker = true;
                        Console.Write("\n>>> -");
                        command = Console.ReadLine();
                        switch (command)
                        {
                            case "get.disk_list":
                                GetDiskList();
                                break;
                            case "select.disk":
                                SelectDisk();
                                break;
                            case "select.directory":
                                SelectDirectory();
                                break;
                            case "select.full_path":
                                SelectFullPath();
                                break;
                            case "view.directory_files":
                                Console.WriteLine();
                                ViewAvailableFiles();
                                break;
                            case "view.directory_folders":
                                Console.WriteLine();
                                ViewAvailableFolders();
                                break;
                            case "view.current_path":
                                ViewFullPath();
                                break;
                            case "print.file_content_utf8":
                                PrintEncodingUTF8();
                                break;
                            /*case "print.file_content":
                                break;*/
                            case "copy.file":
                                CopyFile();
                                break;
                            case "move.file":
                                MoveFile();
                                break;
                            case "delete.file":
                                DeleteFile();
                                break;
                            case "create.file_utf8":
                                CreateFileUTF8();
                                break;
                            /*case "create.file":
                                break;*/
                            case "concatenate.files":
                                ConcatenateFiles();
                                break;
                            case "clean.screen":
                                Clean();
                                break;
                            case "help":
                                Information();
                                break;
                            case "exit":
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("\nОшибка! Такой команды не существует...\nПовторите ввод");
                                command_checker = false;
                                break;
                        }
                    } while (!command_checker);
                } while (!exit);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка метода Main: " + ex.Message);
            }
        }

        /// <summary>
        /// Конкатенация файлов. Принимаются пути первого и второго файла, путь к папке, в которую
        /// пользователь хочет записать сконкатенированный файл, и имя сконкатенированного файла.
        /// </summary>
        private static void ConcatenateFiles()
        {
            try
            {
                string filePath1;
                bool isFilePath1Correct;
                do
                {
                    Console.Write("\nВведите полный путь первого файла для конкатенации: ");
                    filePath1 = Console.ReadLine();
                    isFilePath1Correct = IsFileCorrect(filePath1);
                } while (!isFilePath1Correct);

                string filePath2;
                bool isFilePath2Correct;
                do
                {
                    Console.Write("\nВведите полный путь второго файла для конкатенации: ");
                    filePath2 = Console.ReadLine();
                    isFilePath2Correct = IsFileCorrect(filePath2);
                } while (!isFilePath2Correct);

                string[] srcFileNames = { filePath1, filePath2 };

                string concatenatedFolderPath;
                bool isConcatenatedFolderPathCorrect;
                do
                {
                    Console.Write("\nВведите полный путь папки, в которую нужно поместить результирующий файл: ");
                    concatenatedFolderPath = Console.ReadLine();
                    isConcatenatedFolderPathCorrect = IsFolderCorrect(concatenatedFolderPath);
                } while (!isConcatenatedFolderPathCorrect);

                string newFile, concatenatedFilePath;
                bool isConcatenatedFilePathCorrect;
                do
                {
                    Console.Write("\nВведите название файла с тем же расширением, что и у конкатинируемых файлов: ");
                    newFile = Console.ReadLine();
                    char[] concatPath = new char[concatenatedFolderPath.Length + 1 + newFile.Length];
                    isConcatenatedFilePathCorrect = Path.TryJoin(concatenatedFolderPath, "\\", newFile, concatPath, out _);
                    if (!isConcatenatedFilePathCorrect)
                        Console.WriteLine("Ошибка ввода. Название файла введено некорректно " +
                                          "(возможно, отсутствует расширение)...");
                    concatenatedFilePath = new string(concatPath);
                } while (!isConcatenatedFilePathCorrect);

                Stream currentStream = File.OpenWrite(concatenatedFilePath);
                foreach (string srcFileName in srcFileNames)
                {
                    Stream srcStream = File.OpenRead(srcFileName);
                    srcStream.CopyTo(currentStream);
                }
                currentStream.Close();

                var sr = new StreamReader(concatenatedFilePath);
                Console.WriteLine("\nРезультат конкатенации: " + sr.ReadToEnd());
                sr.Close();
                currentStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при конкатенации двух файлов: " + ex.Message);
            }
        }

        /// <summary>
        /// Создание текстового файла в формате UTF-8. Принимается путь папки, где нужно создать искомый файл,
        /// затем обрабатывается на корректность и создаётся требуемый файл.
        /// </summary>
        private static void CreateFileUTF8()
        {
            try
            {
                string folderPath;
                bool isPathCorrect;
                do
                {
                    Console.Write("\nУкажите полный путь папки, в которой нужно создать текстовый файл в кодировке UTF-8: ");
                    folderPath = Console.ReadLine();
                    isPathCorrect = IsFolderCorrect(folderPath);
                } while (!isPathCorrect);

                string newFile, newPath;
                bool isFolderWithFileCorrect;
                do
                {
                    Console.Write("\nВведите название файла с расширением '.txt': ");
                    newFile = Console.ReadLine();
                    char[] concatPath = new char[folderPath.Length + 1 + newFile.Length];
                    isFolderWithFileCorrect = Path.TryJoin(folderPath, "\\", newFile, concatPath, out _);
                    if (!isFolderWithFileCorrect)
                        Console.WriteLine("Ошибка ввода. Название файла введено некорректно " +
                                          "(возможно, отсутствует расширение)...");
                    newPath = new string(concatPath);
                } while (!isFolderWithFileCorrect);

                if (String.Equals(Path.GetExtension(newPath), ".txt"))
                {
                    File.Create(newPath);
                    Console.WriteLine("\nПроцесс создания прошёл успешно!");
                }
                else
                {
                    Console.WriteLine("\nОтмена процесса создания.\nВозможные причины: расширение файла " +
                                      "не является расширением '.txt'...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка создания файла в кодировке UTF-8: " + ex);
            }
        }

        /// <summary>
        /// Вывод в консоль текстового файла в кодировке UTF-8. Принимается путь папки, в котором находится папка
        /// с искомым файлом, затем обрабатывается на корректность и выводится на экран.
        /// </summary>
        private static void PrintEncodingUTF8()
        {
            try
            {
                string filePath;
                bool isPathCorrect;
                do
                {
                    Console.Write("\nУкажите текущий полный путь выводимого в кодировке файла для вывода " +
                          "на экран в кодировке UTF-8:");
                    filePath = Console.ReadLine();
                    isPathCorrect = IsFileCorrect(filePath);
                } while (!isPathCorrect);

                var readingFile = new StreamReader(filePath);

                Console.WriteLine("\nСодержимое файла:\n");
                Console.WriteLine(readingFile.ReadToEnd());
                readingFile.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при выводе содержимого файла в кодировке UTF-8: " + ex.Message);
            }
        }

        /// <summary>
        /// Удаление файла.
        /// </summary>
        private static void DeleteFile()
        {
            try
            {
                string deletedFile;
                bool isFileCorrect;
                do
                {
                    Console.Write("\nУкажите текущий полный путь удаляемого файла: ");
                    deletedFile = Console.ReadLine();
                    isFileCorrect = IsFileCorrect(deletedFile);
                } while (!isFileCorrect);

                File.Delete(deletedFile);
                Console.WriteLine("\nПроцесс удаления прошёл успешно!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникла ошибка при удалении файла: " + ex.Message);
            }
        }

        /// <summary>
        /// Перемещение файла.
        /// </summary>
        private static void MoveFile()
        {
            try
            {
                string movingFile;
                bool isFileCorrect;
                do
                {
                    Console.Write("\nУкажите текущий полный путь перемещаемого файла: ");
                    movingFile = Console.ReadLine();
                    isFileCorrect = IsFileCorrect(movingFile);
                } while (!isFileCorrect);

                string newFolder;
                bool isFolderCorrect;
                do
                {
                    Console.Write("\nУкажите полный путь папки (без указания имени файла)," +
                                  "в которую нужно переместить файл: ");
                    newFolder = Console.ReadLine();
                    isFolderCorrect = IsFolderCorrect(newFolder);
                } while (!isFolderCorrect);

                string newFile, newPath;
                bool isFolderWithFileCorrect;
                do
                {
                    Console.Write("\nВведите название файла с тем же расширением, что и у перемещаемого файла: ");
                    newFile = Console.ReadLine();
                    char[] concatPath = new char[newFolder.Length + 1 + newFile.Length];
                    isFolderWithFileCorrect = Path.TryJoin(newFolder, "\\", newFile, concatPath, out _);
                    if (!isFolderWithFileCorrect)
                        Console.WriteLine("Ошибка ввода. Название файла введено некорректно " +
                                          "(возможно, отсутствует расширение)...");
                    newPath = new string(concatPath);
                } while (!isFolderWithFileCorrect);

                if (String.Equals(Path.GetExtension(movingFile), Path.GetExtension(newPath)) && !String.Equals(movingFile, newPath))
                {
                    File.Move(movingFile, newPath, true);
                    Console.WriteLine("\nПроцесс перемещения прошёл успешно!");
                }
                else
                {
                    Console.WriteLine("\nОтмена процесса перемещения.\nВозможные причины: расширения файлов " +
                                      "различны или перемещение файла в папку, в которой он уже находится...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникла ошибка при перемещении файла: " + ex.Message);
            }
        }

        /// <summary>
        /// Проверка пути к папке на корректность и существование на ПК.
        /// </summary>
        /// <param name="inputPath"></param>
        /// <returns> true - путь корректный, false - путь некорректный. </returns>
        private static bool IsFolderCorrect(string inputPath)
        {
            if (Directory.Exists(inputPath) == false)
            {
                Console.WriteLine("\nОшибка ввода. Повторите попытку...\nВозможные причины:" +
                                  "\n1) Введёный путь отсутствует;" +
                                  "\n2) Введённый путь неполный;" +
                                  "\n3) Введён путь к файлу, а не к папке.");
                return false;
            }
            else if (Path.IsPathFullyQualified(inputPath) == false)
            {
                Console.WriteLine("\nВведён неполный путь. Повторите попытку...");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Копирование файла.
        /// </summary>
        private static void CopyFile()
        {
            try
            {
                string copiedFile;
                bool isOldFileCorrect;
                do
                {
                    Console.Write("\nУкажите текущий полный путь копируемого файла: ");
                    copiedFile = Console.ReadLine();
                    isOldFileCorrect = IsFileCorrect(copiedFile);
                } while (!isOldFileCorrect);

                string newFolder;
                bool isFolderCorrect;
                do
                {
                    Console.Write("\nУкажите полный путь папки (без указания имени файла)," +
                                  "в которую нужно копировать файл: ");
                    newFolder = Console.ReadLine();
                    isFolderCorrect = IsFolderCorrect(newFolder);
                } while (!isFolderCorrect);

                string newFile, newPath;
                bool isFolderWithFileCorrect;
                do
                {
                    Console.Write("\nВведите название файла с тем же расширением, что и у копируемого файла: ");
                    newFile = Console.ReadLine();
                    char[] concatPath = new char[newFolder.Length + 1 + newFile.Length];
                    isFolderWithFileCorrect = Path.TryJoin(newFolder, "\\", newFile, concatPath, out _);
                    if (!isFolderWithFileCorrect)
                        Console.WriteLine("Ошибка ввода. Название файла введено некорректно " +
                                          "(возможно, отсутствует расширение)...");
                    newPath = new string(concatPath);
                } while (!isFolderWithFileCorrect);

                if (String.Equals(Path.GetExtension(copiedFile), Path.GetExtension(newPath)) && !String.Equals(copiedFile, newPath))
                {
                    File.Copy(copiedFile, newPath, true);
                    Console.WriteLine("\nПроцесс копирования прошёл успешно!");
                }
                else
                {
                    Console.WriteLine("\nОтмена процесса копирования.\nВозможные причины: расширения файлов " +
                                      "различны или копирование файла самого в себя...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при копировании файла: " + ex.Message);
            }
        }

        /// <summary>
        /// Проверка пути к файлу на корректность и существование на ПК.
        /// </summary>
        /// <param name="inputPath"></param>
        /// <returns> true - путь корректный, false - путь некорректный. </returns>
        private static bool IsFileCorrect(string inputPath)
        {
            if (File.Exists(inputPath) == false)
            {
                Console.WriteLine("\nОшибка ввода. Повторите попытку...\nВозможные причины:" +
                    "\n1) Введёный путь отсутствует;" +
                    "\n2) Введённый путь неполный;" +
                    "\n3) Введён путь к папке, а не к файлу (не забудьте указать в пути сам файл с расширением!).");
                return false;
            }
            else if (Path.IsPathFullyQualified(inputPath) == false)
            {
                Console.WriteLine("\nВведён неполный путь. Повторите попытку...");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Изменение текущей отображаемой директории для перемещения.
        /// </summary>
        private static void SelectFullPath()
        {
            try
            {
                string newPath;
                do
                {
                    Console.Write("\nУкажите новый полный путь к какой-либо папке: ");
                    newPath = Console.ReadLine();

                    if (Directory.Exists(newPath) == false)
                        Console.WriteLine("\nОшибка ввода. Повторите попытку...\nВозможные причины:" +
                            "\n1) Введёный путь отсутствует;" +
                            "\n2) Введённый путь неполный;" +
                            "\n3) Введён путь к файлу, а не к папке.");
                    else if (Path.IsPathFullyQualified(newPath) == false)
                        Console.WriteLine("\nВведён неполный путь. Повторите попытку...");
                    else
                    {
                        CurrentFullPath = newPath;
                    }
                } while (Directory.Exists(newPath) == false || Path.IsPathFullyQualified(newPath) == false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при указании нового полного пути: " + ex.Message);
            }
        }

        /// <summary>
        /// Отображение текущей директории для перемещения.
        /// </summary>
        private static void ViewFullPath() => Console.WriteLine(Environment.NewLine + CurrentFullPath);

        /// <summary>
        /// Просмотр подпапок в текущей отображаемой директории для перемещения.
        /// </summary>
        private static void ViewAvailableFolders()
        {
            try
            {
                int num = 1;
                string[] folder_list = Directory.GetDirectories(CurrentFullPath);

                if (folder_list.Length == 0)
                {
                    Console.WriteLine("В текущей директории папки отсутствуют...");
                    return;
                }

                foreach (string folder in folder_list)
                {
                    Console.WriteLine($"{num}) {folder}");
                    num += 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при попытке просмотра доступных папок: " + ex.Message);
            }
        }

        /// <summary>
        /// Просмотр подфайлов в текущей отображаемой директории для перемещения.
        /// </summary>
        private static void ViewAvailableFiles()
        {
            try
            {
                int num = 1;
                string[] file_list = Directory.GetFiles(CurrentFullPath);

                if (file_list.Length == 0)
                {
                    Console.WriteLine("В текущей директории файлы отсутствуют...");
                    return;
                }

                foreach (string file in file_list)
                {
                    Console.WriteLine($"{num}) {file}");
                    num += 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при попытке просмотра доступных файлов: " + ex.Message);
            }
        }

        /// <summary>
        /// Выбор папки из предлагаемого списка подпапок и запись её пути вместо текущей отображаемой
        /// директории для перемещения или же подъём на одну папку вверх.
        /// </summary>
        private static void SelectDirectory()
        {
            try
            {
                Console.WriteLine("\nСписок доступных папок:\n-----------------------");
                ViewAvailableFolders();
                Console.WriteLine("\nСписок файлов:\n--------------");
                ViewAvailableFiles();

                string tempFullPath = string.Copy(CurrentFullPath);
                string[] folder_list = Directory.GetDirectories(CurrentFullPath);

                do
                {
                    CurrentFullPath = tempFullPath;

                    if (folder_list.Length == 0)
                    {
                        Console.Write("\nСписок доступных папок пуст. Нажмите любую клавишу, чтобы остаться в текущей папке," +
                                      "\nили нажмите клавишу 'Backspace', чтобы подняться на уровень вверх...");
                        if (Console.ReadKey(true).Key == ConsoleKey.Backspace)
                        {
                            CurrentFullPath = Path.GetDirectoryName(CurrentFullPath);
                        }
                    }
                    else
                    {
                        int num;
                        bool isCorrect;

                        do
                        {
                            Console.Write("\nВведите номер папки из списка или поднимитесь на уровень вверх (введите -1): ");
                            isCorrect = int.TryParse(Console.ReadLine(), out num);
                            if (!isCorrect || num < -1 || num == 0 || num > folder_list.Length)
                                Console.WriteLine("Некорректные данные! Повторите попытку...");
                        } while (!isCorrect || num < -1 || num == 0 || num > folder_list.Length);

                        if (num == -1)
                            CurrentFullPath = Path.GetDirectoryName(CurrentFullPath);
                        else
                            CurrentFullPath = folder_list[num - 1];
                        if (CurrentFullPath == null)
                            Console.WriteLine("\nПодъём наверх невозможен, поскольку текущая директория указывает на диск...");
                    }
                } while (CurrentFullPath == null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при перемещении по директории вверх/вниз: " + ex.Message);
            }
        }

        /// <summary>
        /// Выбор диска и запись вместо текущей отображаемой директории для перемещения.
        /// </summary>
        private static void SelectDisk()
        {
            try
            {
                GetDiskList();

                DriveInfo[] disk_list = DriveInfo.GetDrives();
                int num;
                bool isCorrect;

                do
                {
                    Console.Write("\nВведите номер выбранного диска: ");
                    isCorrect = int.TryParse(Console.ReadLine(), out num);
                    if (!isCorrect || num <= 0 || num > disk_list.Length)
                        Console.WriteLine("Некорректные данные! Повторите попытку...");
                } while (!isCorrect || num <= 0 || num > disk_list.Length);

                CurrentFullPath = $"{disk_list[num - 1]}";
                Console.WriteLine($"\nДиск {disk_list[num - 1]} успешно выбран.");
                ViewFullPath();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при выборе диска: " + ex.Message);
            }
        }

        /// <summary>
        /// Просмотр списка доступных дисков.
        /// </summary>
        private static void GetDiskList()
        {
            DriveInfo[] disk_list = DriveInfo.GetDrives();
            int num = 1;

            Console.WriteLine("\nТекущие диски на ПК:");
            foreach (DriveInfo el in disk_list)
            {
                Console.WriteLine($"{num}) {el}");
                num += 1;
            }
        }

        /// <summary>
        /// Очистка экрана от записей.
        /// </summary>
        private static void Clean()
        {
            try
            {
                Console.WriteLine("Очистка экрана...");

                Console.WriteLine(3);
                Thread.Sleep(1000);

                Console.WriteLine(2);
                Thread.Sleep(1000);

                Console.WriteLine(1);
                Thread.Sleep(1000);

                Console.Clear();
                Console.WriteLine("Экран очищен успешно, программа готова к работе.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при очистке экрана: " + ex);
            }
        }

        /// <summary>
        /// Информация о всех возможностях файлового менеджера.
        /// </summary>
        private static void Information()
        {
            Console.WriteLine("\n|-------------------------------|");
            Console.WriteLine("|Возможности файлового менеджера|");
            Console.WriteLine("|-------------------------------|");

            Console.WriteLine("\nДополнительные команды: ");

            Console.WriteLine("\n'clean.screen' - очистка экрана от текущий записей.");
            Console.WriteLine("'exit' - завершение работы с программой.");
            Console.WriteLine("'help' - команда, которую Вы вызвали только что, чтобы получить " +
                              "дополнительные сведения по использованию. =)");

            Console.WriteLine("\n--------------------------------------------------------------------------------------" +
                              "----------------------");

            Console.WriteLine("\nОсновные команды:");

            Console.WriteLine("\nК каждому пункту, описывающему действие, приведён список команд. " +
                              "Наименования команд были составлены по принципу 'глагол.существительное' на латинице.");

            Console.WriteLine("\n1. просмотр списка дисков компьютера и выбор диска:");
            Console.WriteLine("'get.disk_list' - просмотр списка дисков;\n'select.disk' - выбор диска.");

            Console.WriteLine("\n2. переход в другую директорию (выбор папки):");
            Console.WriteLine("'select.directory' - ступенчатый шаг в текущей директории вниз (выбор подпапки из списка " +
                              "доступных в текущей папке) или вверх (выход из текущей папки и переход в папку на один " +
                              "уровень выше);\n'select.full_path' - указать полный путь к папке.");

            Console.WriteLine("\n3. просмотр списка файлов в директории:");
            Console.WriteLine("'view.directory_files' - просмотр списка файлов;\n'view.directory_folders' - просмотр " +
                              "списка папок;\n'view.current_path' - просмотр текущего полного пути.");

            Console.WriteLine("\n4. вывод содержимого текстового файла в консоль в кодировке UTF-8:");
            Console.WriteLine("'print.file_content_utf8' - вывод содержимого файла в кодировке UTF - 8.");

            Console.WriteLine("\n6. копирование файла:");
            Console.WriteLine("'copy.file' - копирование файла в существующий с тем же расширением. " +
                              "Возможна перезапись файла.\nПримечание: копирование файла под тем же именем в ту же " +
                              "директорию(т.е.копирование файла самого в себя) невозможно.");

            Console.WriteLine("\n7. перемещение файла в выбранную пользователем директорию:");
            Console.WriteLine("'move.file' - перемещение файла по полному пути с возможностью переименовывания самого " +
                              "файла.\nПримечание: исходный файл не может быть перемещён с исходным именем в ту же " +
                              "директорию, в которой он находился изначально.");

            Console.WriteLine("\n8. удаление файла:");
            Console.WriteLine("'delete.file' - удаление файла по указанной директории.");

            Console.WriteLine("\n9. создание простого текстового файла в кодировке UTF-8:");
            Console.WriteLine("'create.file_utf8' - создание текстового файла в кодировке UTF - 8.");

            Console.WriteLine("\n11.конкатенация содержимого двух или более текстовых файлов и вывод " +
                              "результата в консоль в кодировке UTF-8.");
            Console.WriteLine("'concatenate.files' - конкатенация двух файлов и вывод результата " +
                              "в консоль в кодировке UTF - 8. (Несколько файлов можно конкатенировать вызовом " +
                              "соотвествующей команды тем числом раз, которое будет необходимо.");
        }
    }
}
