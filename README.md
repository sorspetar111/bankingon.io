# bankingon.io
bankingon.io


PS \bankingon.io> dotnet run --project BankingFeesCalculator
Banking Fees Calculator (NET 8)
Plan (Standard/Gold/Premium): Standard
Has salary transfer? (y/n): y
Is student? (y/n): n
Internal transfers count: : 1
Interbank transfers count: : 1
Immediate transfers count: : 1
ATM withdrawals (same bank): : 0.1
Enter a valid integer (0 if none). Try again.
ATM withdrawals (same bank): : 0
ATM withdrawals (other bank): : 0
ATM withdrawals (abroad): : 0
Card payments count: : 1
Overdraft used (amount): 0
Other fees: 0
Discounts: 0
Total monthly fees: $11.69
PS \bankingon.io> cd .\BankingFeesCalculator\
Banking Fees Calculator (NET 8)
Plan (Standard/Gold/Premium): Standard
Has salary transfer? (y/n): y
Is student? (y/n): n
Internal transfers count: : 1
Interbank transfers count: : 1
Immediate transfers count: : 1
ATM withdrawals (same bank): : 0.1
Enter a valid integer (0 if none). Try again.
ATM withdrawals (same bank): : 0
ATM withdrawals (other bank): : 0
ATM withdrawals (abroad): : 0
Card payments count: : 1
Overdraft used (amount): 0
Other fees: 0
Discounts: 0
Total monthly fees: $11.69
PS \bankingon.io> cd .\BankingFeesCalculator\
PS \bankingon.io\BankingFeesCalculator> dotnet run
Banking Fees Calculator (NET 8)
Plan (Standard/Gold/Premium):
Has salary transfer? (y/n): y
Is student? (y/n): n
Internal transfers count: : 1
Interbank transfers count: : 1
Immediate transfers count: : 1
ATM withdrawals (same bank): : 0.1
Enter a valid integer (0 if none). Try again.
ATM withdrawals (same bank): : 0
ATM withdrawals (other bank): : 0
ATM withdrawals (abroad): : 0
Card payments count: : 1
Overdraft used (amount): 0
Other fees: 0
Discounts: 0
Total monthly fees: $11.69
PS \bankingon.io> cd .\BankingFeesCalculator\
ATM withdrawals (same bank): : 0.1
Enter a valid integer (0 if none). Try again.
ATM withdrawals (same bank): : 0
ATM withdrawals (other bank): : 0
ATM withdrawals (abroad): : 0
Card payments count: : 1
Overdraft used (amount): 0
Other fees: 0
Discounts: 0
Total monthly fees: $11.69
PS \bankingon.io> cd .\BankingFeesCalculator\
Card payments count: : 1
Overdraft used (amount): 0
Other fees: 0
Discounts: 0
Total monthly fees: $11.69
PS \bankingon.io> cd .\BankingFeesCalculator\
Total monthly fees: $11.69
PS \bankingon.io> cd .\BankingFeesCalculator\
PS \bankingon.io\BankingFeesCalculator> dotnet run
Banking Fees Calculator (NET 8)
Plan (Standard/Gold/Premium):
PS \bankingon.io\BankingFeesCalculator> dotnet run
Banking Fees Calculator (NET 8)
Plan (Standard/Gold/Premium):
PS \bankingon.io\BankingFeesCalculator>  dotnet build
Restore complete (0.5s)
PS \bankingon.io\BankingFeesCalculator>  dotnet build
Restore complete (0.5s)
  BankingFeesCalculator net8.0 succeeded (0.2s) → bin\Debug\net8.0\BankingFeesCalculator.dll

Build succeeded in 1.2s
PS \bankingon.io\BankingFeesCalculator> cd ..
PS \bankingon.io> cd .\OffersLoader\
PS \bankingon.io\OffersLoader>  dotnet build
Restore complete (0.5s)
  OffersLoader net8.0 succeeded (0.3s) → bin\Debug\net8.0\OffersLoader.dll

Build succeeded in 1.3s
PS C:\DW\Work\bankingon.io\OffersLoader>
PS C:\DW\Work\bankingon.io\OffersLoader> 


Задача 1. Да се напише конзолно приложение - калкулатор на месечни банкови
такси.
Входни параметри: план на клиента (Standard, Gold, Premium), има ли превод на заплата
по сметката (да/не), студент ли е (да/не), брой вътрешнобанкови преводи, брой
междубанкови преводи, брой незабавни преводи, брой тегления на банкомат на същата
банка, брой тегления от банкомат на друга банка, брой тегления в чужбина, брой
плащания с карта, използван овърдрафт (сума), други такси, отстъпки. Общата дължима
сума S се смята по слените правила:
● прибавя се месечната такса според плана
o Standard - 5.99
o Gold - 9.99
o Premium - 19.99
▪ Ако клиентът има превод на заплата по сметка:
● Месечната такса отпада само при Gold плана
● При Standard и Premium месечната такса НЕ отпада
● Прибавя се броят вътрешнобанкови преводи:
o При Standard първите 3 са безплатни, всеки следващ се таксува по 0.20.
o При Gold и Premium всички са безплатни
● Прибавя се броят междубанкови преводи:
o Standard - 1.20 за всеки
o Gold - 0.80 за всеки
o Premium - безплатни
● Прибавя се броят незабавни преводи:
o 2.50 за всеки, независимо от плана
● Прибавят се тегленията от банкомат:
o От същата банка - безплатни
o От друга банка:
▪ Standard - 0.50
▪ Gold - 0.20
▪ Premium - безплатни
o В чужбина:
▪ Standard - 3.00
▪ Gold - 2.00
▪ Premium - безплатни
● Ако броят плащания с карта е по-малък от 5:
o Начислява се допълнителна такса от 2.00 за всички планове освен
Premium
● Ако използваният овърдрафт е над 0:
o Начислява се 5% върху използваната сума
● Прибавят се другите такси
● Изваждат се отстъпките
● Ако клиентът е студент:
 Крайната сума се намалява с 20%
Пример:
● План: Standard
● Има заплата по сметката: да
● Студент: да
● Вътрешнобанкови преводи: 5
● Междубанкови преводи: 2
● Незабавни преводи: 1
● Тегления от собствена банка: 2
● Тегления от друга банка: 3
● Тегления в чужбина: 1
● Плащания с карта: 3
● Използван овърдрафт: 200.00
● други такси: 1.50
● отстъпки: 3.00
Допълнителни изисквания:
● Да се използва подходящ тип за парични стойности
● Да се осигури възможност за добавяне на нов клиентски план без промяна на
съществуващата бизнес логика
● Да се осигури възможност за добавяне на нов тип такса
● Кодът да бъде структуриран така, че да бъде лесен за разширяване и поддръжка


use C# .net native 8 project, EF Core and FluentAPI.
Create a new console project with db context. Database engine Microsoft SQL server.
Create serilization and deserialization of offers.csv.
Да се напише конзолно приложение, което да налее данните от offers.csv
файла да се вкарат в база в таблица със същото име и колони.

Задача 3. Трябва да имплементирате код, който симулира зоологическа градина.
● Зоологическата градина съдържа животни. Животните в зоопарка са от 3 различни
вида: маймуни, лъвове, слонове. Всяко животно има здравна стойност (health points),
представена с точки в диапазона от 0 до 100.
● Трябва да има метод който симулира "огладняването" на животните. Гладът намалява
здравето на животните. Когато се извика този метод, за всяко животно в зоопарка се
генерира произволна стойност между 0 и 20, която се използва за намаляване на
здравето на това животно.
● Трябва да има метод за симулиране на храненето на животните. Когато се извика
този метод, за всеки от трите вида се генерира произволна стойност между 5 и 25.
Тогава здравето на всяко животно в зоопарка се увеличава от стойността,
генерирана за неговия вид.
● Всеки вид има специфично състояние на смърт. Маймуната умира, когато нейните
здравни точки паднат под 40. Лъвът умира, когато здравните му точки паднат под 50.
Слонът не може да ходи, докато има по-малко от 70 здравни точки. Ако слонът не
може да ходи, когато здравето му трябва да бъде намалено (без значение колко), той
умира.
● Трябва да има метод, който да връща броя животни, които все още са живи в
зоопарка.
● Зоопаркът започва с 5 животни за всеки вид. Всяко животно започва със 100 точки за здраве.



offers.csv

