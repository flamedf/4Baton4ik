
#include <iostream>


class list {
	class element {

		char info;
		element* next;
	public:
		element(char, element* = NULL);
		char GetValue();
		void SetValue(char);
		element* GetPointer();
		int toEnd();
		void SetPointer(element*);
	};
protected:
	element* head;
	int FullSize;
public:

	list();
	list(const list&);
	~list();

	bool IsEmpty();
	void AddList(char, int);
	void DelList(int);
	friend std::ostream& operator<<(std::ostream&, list&);
	friend std::istream& operator>>(std::istream&, list&);
	void operator()(int);
	void operator()(char, int);
	bool operator==(list&);
	bool operator!=(list&);
};
list::element::element(const char _a, element* _next) 
{
	info = _a;
	next = _next;
}

char list::element::GetValue() { return info; }
void list::element::SetValue(char value) { info = value; }
list::element* list::element::GetPointer() { return next; }
void list::element::SetPointer(list::element* ptr) { next = ptr; }
int list::element::toEnd() { if (next == NULL) return 1; else return next->toEnd() + 1; }
list::list() { head = NULL; FullSize = 0; }

list::list(const list& list1) {
	head = NULL;
	element * cur = list1.head;
	int n = 1;
	while (cur != NULL) {
		AddList(cur->GetValue(), n++);
		cur = cur->GetPointer();
	}
}
list::~list() {
	element* cur, * help;
	cur = head;
	while (cur != NULL) {
		help = cur->GetPointer();
		delete cur;
		cur = help;
	}
}
bool list::IsEmpty() {
	if (head == NULL) return true;
	else return false;
}
void list::AddList(char key, int pos = 1) {
	element* current, * help;

	if (pos == 1) {
		help = new element(key, head);
		head = help;
		FullSize++;
		return;
	}
	current = head;
	int i = 1;
	while (i != pos -1 && current->GetPointer() != NULL) {
		i++;
		current = current->GetPointer();
	}
	if (current == NULL) {
		std::cout << "inccorect position";
		return;
	}
	help = new element(key, current->GetPointer());
	current->SetPointer(help);
	FullSize++;
}
void list::DelList(int pos = 1) 
{
	element* current, *next;

	if (pos == 1) {
		head = head->GetPointer();
		return;
	}
	current = head;
	next = head->GetPointer();
	
	int i = 1;
	while (i != pos - 1 && next != NULL) {
		i++;
		current = current->GetPointer();
		next = next->GetPointer();
	}
	if (next == NULL) {
		std::cout << "inccorect position";
		return;
	}
	current->SetPointer(next->GetPointer());
	FullSize--;
}
std::ostream& operator<<(std::ostream& out, list &list1)
{
	if (list1.IsEmpty()) { out << "List is empty"; return out; }
	list::element* current = list1.head;
	while (current->GetPointer() != NULL)
	{
		out << current->GetValue();
		current = current->GetPointer();
	}
	out << current->GetValue() << '\0';
	return out;
}
std::istream& operator>>(std::istream& in, list &list1) 
{
	list1 = list();
	char buf;
	in >> buf;
	while (buf != 3 && buf != 4 && buf != 0)
	{
		list1.AddList(buf,list1.FullSize + 1);
		//std::cout << " " << buf << " " << list1.FullSize<< std::endl;
		in >> buf;
	}
	return in;
}
void list::operator()(int id) {DelList(id);}
void list::operator()(char key, int id) { AddList(key,id); }
bool list::operator==(list &_list) 
{
	list::element* curr = head;
	list::element* _curr = _list.head;
	if (FullSize != _list.FullSize) return false;
	while (curr != NULL)
	{
		if (curr->GetValue() != _curr->GetValue()) return false;
		curr = curr->GetPointer();
		_curr = _curr->GetPointer();
	}
	return true;
}
bool list::operator!=(list &_list) 
{
	element* curr = head;
	element* _curr = _list.head;
	if (FullSize != _list.FullSize) return true;
	while (curr != NULL)
	{
		if (curr->GetValue() != _curr->GetValue()) return true;
		curr = curr->GetPointer();
		_curr = _curr->GetPointer();
	}
	return false;
}
int main()
{
	list lst1 = list();
	list lst2 = list();
	lst1('a', 1);
	lst1('b', 1);
	lst1('c', 1);
	lst2('a', 1);
	lst2('b', 1);
	lst2('c', 1);
	std::cout << (lst1 == lst2) << std::endl;
}

