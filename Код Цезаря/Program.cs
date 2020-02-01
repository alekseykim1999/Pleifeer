using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] alphabet = "АБВГДЕЖЗИКЛМНОПРСТУФХЦЧШЩЪЫЭЮЯ".ToCharArray();
            //ПОЛЕТ
            //КЛКЕПЕШОБКЕРЭЛЧСКУЛЮЕТВМВКИММЮЗОТЖША
            string text; //исходный текст для шифрования

            int i_first = 0, j_first = 0;  //координаты первого символа пары
            int i_second = 0, j_second = 0;//координаты второго символа пары
            string s1 = "", s2 = ""; //строки для хранения зашифрованного символа 
            string encodetString; //зашифрованая строка
            int rows = 5, columns = 6;
            int i, j;
            text = "";
            encodetString = "";
            char[] keyWord;

            keyWord = "ПОЛЕТА".ToCharArray();

            // Создаем таблицу
            var table = new char[rows, columns];

            // Вписываем в нее ключевое слово
            for (i = 0; i < 6; i++)
            {
                table[0, i] = keyWord[i];
            }

            // Исключаем уникальные символы ключевого слова из алфавита
            alphabet = alphabet.Except(keyWord).ToArray();
            int b = 0;
            // Вписываем алфавит
            for (i = 1; i < 5; i++)
            {

                for (j = 0; j < 6; j++)
                {

                    table[i,j] = alphabet[b];
                    b++;
                }
            }

            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 6; j++)
                {
                    Console.Write(table[i, j] + " ");
                }
                Console.WriteLine();
            }

            
            text = "КЛКЕПЕШОБКЕРЭЛЧСКУЛЮЕТВМВКИММЮЗОТЖША";
            Console.WriteLine("КЛКЕПЕШОБКЕРЭЛЧСКУЛЮЕТВМВКИММЮЗОТЖША");
            int t = text.Length; //длина входного слова

            ///проверяем, четное ли число символов в строке
            int temp = t % 2;
            if (temp != 0) //если нет
            {               //то добавляем в конец строки символ " " 
                text = text.PadRight((t + 1), 'Я');
            }

            int len = text.Length / 2; /*длина нового массива -
                                                равная половине длины входного слова
                                                 т.к. в новом масиве каждый элемент будет
                                                   содержать 2 элемента из старого массива*/

            string[] str = new string[len]; //новый массив

            int l = -1; //служебная переменная

            for (i = 0; i < t; i += 2) //в старом массиве шаг равен 2
            {
                l++; //индексы для нового массива
                if (l < len)
                {
                    //Элемент_нового_массива[i] =  Элемент_старого_массива[i] +  Элемент_старого_массива[i+1]
                    str[l] = Convert.ToString(text[i]) + Convert.ToString(text[i + 1]);
                }

            }

            ///координаты очередного найденного символа из каждой пары

            foreach (string both in str)
            {
                for (i = 0; i < rows; i++)
                {
                    for (j = 0; j < columns; j++)
                    {
                        //координаты первого символа пары в исходной матрице
                        if (both[0] == (table[i, j]))
                        {
                            i_first = i;
                            j_first = j;

                        }

                        //координаты второго символа пары в исходной матрице
                        if (both[1] == (table[i, j]))
                        {
                            i_second = i;
                            j_second = j;

                        }
                    }
                }

                ///если пара символов находится в одной строке
                if (i_first == i_second)
                {
                    if (j_first == 0) /*если символ (номер 1) первый в строке,
                                       кодируем его последним символом из матрицы*/
                    {
                        s1 = Convert.ToString(table[i_first, columns - 1]);
                    }
                    //если символ (номер 1) не последний, кодируем его стоящим слева от него
                    else
                    {
                        s1 = Convert.ToString(table[i_first, j_first - 1]);
                    }

                    if (j_second == 0) /*если символ (номер 2) первый в строке
                                       кодируем его последним символом из матрицы*/
                    {
                        s2 = Convert.ToString(table[i_second, columns-1]);
                    }
                    //если символ (номер 2) не последний, кодируем его стоящим слева от него
                    else
                    {
                        s2 = Convert.ToString(table[i_second, j_second - 1]);
                    }

                }

                ///если пара символов находится в одном столбце
                if (j_first == j_second)
                {
                    if (i_first == 0) //если первый символ - верхний, то декодируеся самым нижним (для первого)
                    {
                        s1 = Convert.ToString(table[rows - 1, j_first]);
                    }
                    else // иначе декодируется верхним (для первого)
                    {
                        s1 = Convert.ToString(table[i_first - 1, j_first]);
                    }

                    if (i_second == 0) //если первый символ - верхний, то декодируеся самым нижним (для второго)
                    {
                        s2 = Convert.ToString(table[rows-1, j_second]);
                    }

                    else // иначе декодируется верхним (для второго)
                    {
                        s2 = Convert.ToString(table[i_second - 1, j_second]);
                    }
                }

                ///если пара символов находиться в разных столбцах и строках
                if (i_first != i_second && j_first != j_second)
                {
                    
                    s1 = Convert.ToString(table[i_first, j_second]);
                    s2 = Convert.ToString(table[i_second, j_first]);
                }

                if (s1 == s2) //если оказались равны
                {
                    encodetString = encodetString + s1 + "я" + s2;
                }
                else
                {

                    //записыавем результат кодирования
                    encodetString = encodetString + s1 + s2;
                }

                Console.WriteLine(encodetString.ToLower());
            }
            Console.ReadKey();
        }

    }
}