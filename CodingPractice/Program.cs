using System;

{
    Test1();
    Console.WriteLine("GC 실행 전");
    GC.Collect();
    GC.WaitForPendingFinalizers();
    Console.WriteLine("GC 실행 후");
}

{
    Test2();
    Console.WriteLine("GC 실행 전");
    GC.Collect();
    GC.WaitForPendingFinalizers();
    Console.WriteLine("GC 실행 후");
}

{
    Test3();
    Console.WriteLine("GC 실행 전");
    GC.Collect();
    GC.WaitForPendingFinalizers();
    Console.WriteLine("GC 실행 후");
}

{
    Test4();
    Console.WriteLine("GC 실행 전");
    GC.Collect();
    GC.WaitForPendingFinalizers();
    Console.WriteLine("GC 실행 후");
}

{
    Test5();
    Console.WriteLine("GC 실행 전");
    GC.Collect();
    GC.WaitForPendingFinalizers();
    Console.WriteLine("GC 실행 후");
    Monster.ShowStats();
}

{
    Test6();
    Console.WriteLine("GC 실행 전");
    GC.Collect();
    GC.WaitForPendingFinalizers();
    Console.WriteLine("GC 실행 후");
}

static void Test1()
{
    TestObject obj1 = new TestObject(1);
    TestObject obj2 = new TestObject(2);
}
static void Test2()
{
    Console.WriteLine("프로그램 시작");
    Character hero = new Character("용사");

    hero.Attack();
    hero = null;
}
static void Test3()
{
    Car car1 = new Car("검정");
    car1.Drive();

    Car car2 = new Car("하얀");
    car2.Drive();

    Car car3 = new Car("파랑");
    car3.Drive();

    car1 = null;
    car2 = null;
    car3 = null;
}
static void Test4()
{
    GameSession session = new GameSession("플레이어1");
    session.Play();
    session = null;
}
static void Test5()
{
    Monster m1 = new Monster("슬라임");
    Monster m2 = new Monster("고블린");
    Monster m3 = new Monster("오크");

    Console.WriteLine();
    Monster.ShowStats();
    Console.WriteLine();
    m1 = null;
    m2 = null;
    m3 = null;
}
static void Test6()
{
    Console.WriteLine("=== 아이템 생성 ===");
    GameItem potion = new GameItem("체력 포션", 50);
    GameItem sword = new GameItem("강철 검", 200);
    GameItem shield = new GameItem("나무 방패", 100);

    Console.WriteLine();
    Console.WriteLine("=== 아이템 사용 ===");
    potion.Use();
    sword.Use();

    Console.WriteLine();
    Console.WriteLine("=== 인벤토리 정리 ===");
    potion = null;
    sword = null;
    shield = null;
}

class TestObject
{
    private int _id;

    public TestObject(int id)
    {
        _id = id;
        Console.WriteLine($"객체 {_id} 생성");
    }

    ~TestObject()
    {
        Console.WriteLine($"객체 {_id} 소멸");
    }
}
class Character
{
    private string _name;

    public Character(string name)
    {
        _name = name;
        Console.WriteLine($"[1] {_name} 생성");
    }
    public void Attack()
    {
        Console.WriteLine($"[2] {_name} 공격");
    }

    ~Character()
    {
        Console.WriteLine($"[3] {_name} 소멸");
    }
}
class Car
{
    private string _color;
    public Car(string color)
    {
        _color = color;
        Console.WriteLine($"{_color}색 자동차 조립");
    }

    public void Drive()
    {
        Console.WriteLine($"{_color}색 자동차가 달림");
    }

    ~Car()
    {
        Console.WriteLine($"{_color}색 자동차 폐차");
    }
}
class GameSession
{
    private string _playerName;
    private DateTime _startTime;

    public GameSession(string playerName)
    {
        _playerName = playerName;
        _startTime = DateTime.Now;
        Console.WriteLine($"[{_startTime:HH:mm:ss}] {_playerName} 게임 시작");
    }

    public void Play()
    {
        Console.WriteLine($"{_playerName} 플레이 중...");
    }

    ~GameSession()
    {
        DateTime endTime = DateTime.Now;
        TimeSpan duration = endTime - _startTime;
        Console.WriteLine($"[{endTime:HH:mm:ss}] {_playerName} 게임 종료");
        Console.WriteLine($"플레이 시간: {duration.TotalSeconds:F1}초");
    }
}
class Monster
{
    private static int s_totalCount = 0;
    private static int s_aliveCount = 0;

    private int _id;
    private string _name;

    public Monster(string name)
    {
        s_totalCount++;
        s_aliveCount++;
        _id = s_totalCount;
        _name = name;
        Console.WriteLine($"몬스터 생성: {_name} (ID: {_id})");
        Console.WriteLine($"  - 현재 생존: {s_aliveCount}마리");
    }

    ~Monster()
    {
        s_aliveCount--;
        Console.WriteLine($"몬스터 소멸: {_name} (ID: {_id})");
        Console.WriteLine($"  - 현재 생존: {s_aliveCount}마리");
    }

    public static void ShowStats()
    {
        Console.WriteLine($"총 생성: {s_totalCount}, 현재 생존: {s_aliveCount}");
    }
}
class GameItem
{
    private static int s_nextId = 1;
    private int _id;
    private string _name;
    private int _price;

    public GameItem(string name, int price)
    {
        _id = s_nextId++;
        _name = name;
        _price = price;
        Console.WriteLine($"[생성] 아이템 #{_id}: {_name} ({_price}골드)");
    }

    public void Use()
    {
        Console.WriteLine($"[사용] {_name} 아이템을 사용함");
    }

    ~GameItem()
    {
        Console.WriteLine($"[삭제] 아이템 #{_id}: {_name} 인벤토리에서 제거됨");
    }
}