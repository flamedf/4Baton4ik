// no7.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>

class reader 
{
	//friend std::ostream& operator <<(std::ostream&, std::istream&);
};

std::ostream& operator <<(std::ostream& out, std::istream& in) 
{
	char buf = ' ';
	out << "Transmission started\n";
	while (buf != 3 && buf != 4 && buf != 0)
	{
		in >> buf;
		while (isspace(buf)) 
			in >> buf;
		if (buf == '-')
		{
			out << buf;
			while (buf != 3 && buf != 4 && buf != 0 && buf != '!' && buf != '.' && buf != '?')
			{
				in >> buf;
				out << buf;
			}
		}
		else
		{
			while (buf != 3 && buf != 4 && buf != 0 && buf != '!' && buf != '.' && buf != '?')
			{
				in >> buf;
			}
		}
	}
	return out;
}

int main()
{
	reader Reader;
	std::cout << std::cin;
	std::cout << std::cin;
}
