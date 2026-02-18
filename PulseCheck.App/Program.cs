// Get CPU usage -> top -b -n 1 | grep ^%Cpu
// %Cpu(s):  5,4 us,  3,2 sy,  0,0 ni, 90,3 id,  0,0 wa,  0,0 hi,  1,1 si,  0,0 st 
// Sum 5,4 + 3,2 = ~ CPU 8%

// Get MEM usage -> free -h
// total        used        free      shared  buff/cache   available
// Mem:            31Gi       7,8Gi        12Gi       590Mi        11Gi        23Gi
// Swap:          8,0Gi          0B       8,0Gi
// Should focus on 'free' then get elsewhere total mem, this one is off by 1G

Console.WriteLine("Hello, World!");