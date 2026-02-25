using System;

RunTest();
Console.WriteLine("=== GC 시작 ===");
GC.Collect();
GC.WaitForPendingFinalizers();
Console.WriteLine("=== GC 실행 완료 ===");
Seat.ShowStatus();

static void RunTest()
{
    Seat s1 = new Seat("김민수");
    Seat s2 = new Seat("이지영");
    Seat s3 = new Seat("박서준");

    s1.Study();
    s2.Study();
    s3.Study();

    Seat.ShowStatus();
}

class Seat
{
    private static int SeatUseCount;
    private static int CurrentSeatCount;

    private readonly int ID;
    private string name;

    static Seat()
    {
        SeatUseCount = 0;
        CurrentSeatCount = 0;
    }
    public Seat(string name)
    {
        ID = ++SeatUseCount;
        ++CurrentSeatCount;
        this.name = name;
        Console.WriteLine($"좌석 {ID}번 착석: {name}");
    }

    ~Seat()
    {
        CurrentSeatCount--;
        Console.WriteLine($"좌석 {ID}번 반납: {name}");
    }

    public void Study()
    {
        Console.WriteLine($"{name}이(가) 좌석 {ID}번에서 공부 중...");
    }

    public static void ShowStatus()
    {
        Console.WriteLine($"총 이용: {SeatUseCount}명, 현재 착석: {CurrentSeatCount}명");
    }
}