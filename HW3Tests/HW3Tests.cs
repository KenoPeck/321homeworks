namespace HW3.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
             
        }

        [Test]
        public void SaveToFileTest()
        {
            string teststring = "string for testing";
            File.WriteAllText("savetestfile.txt", teststring);
            Assert.That(File.ReadAllText("savetestfile.txt") == teststring);
        }

        [Test]
        public void LoadFromFileTest()
        {
            HW3Form testForm = new();
            StreamReader testSR = new(File.OpenRead("testfile.txt"));
            testForm.loadText(testSR);
            Assert.That(testForm.getTextbox() == File.ReadAllText("testfile.txt"));
        }

        [Test]
        public void LoadFibonacciNumbersFirst50Test()
        {
            FibonacciTextReader testFib1 = new(50); // create FibonacciTextReader with max of 50 lines
            string fib50 = "1: 0\r\n2: 1\r\n3: 1\r\n4: 2\r\n5: 3\r\n6: 5\r\n7: 8\r\n8: 13\r\n9: 21\r\n10: 34\r\n11: 55\r\n12: 89\r\n13: 144\r\n14: 233\r\n15: 377\r\n16: 610\r\n17: 987\r\n18: 1597\r\n19: 2584\r\n20: 4181\r\n21: 6765\r\n22: 10946\r\n23: 17711\r\n24: 28657\r\n25: 46368\r\n26: 75025\r\n27: 121393\r\n28: 196418\r\n29: 317811\r\n30: 514229\r\n31: 832040\r\n32: 1346269\r\n33: 2178309\r\n34: 3524578\r\n35: 5702887\r\n36: 9227465\r\n37: 14930352\r\n38: 24157817\r\n39: 39088169\r\n40: 63245986\r\n41: 102334155\r\n42: 165580141\r\n43: 267914296\r\n44: 433494437\r\n45: 701408733\r\n46: 1134903170\r\n47: 1836311903\r\n48: 2971215073\r\n49: 4807526976\r\n50: 7778742049\r\n";
            Assert.That(fib50, Is.EqualTo(testFib1.ReadToEnd()));
        }

        [Test]
        public void LoadFibonacciNumbersFirst100Test()
        {
            FibonacciTextReader testFib2 = new(100); // create FibonacciTextReader with max of 100 lines
            string fib100 = "1: 0\r\n2: 1\r\n3: 1\r\n4: 2\r\n5: 3\r\n6: 5\r\n7: 8\r\n8: 13\r\n9: 21\r\n10: 34\r\n11: 55\r\n12: 89\r\n13: 144\r\n14: 233\r\n15: 377\r\n16: 610\r\n17: 987\r\n18: 1597\r\n19: 2584\r\n20: 4181\r\n21: 6765\r\n22: 10946\r\n23: 17711\r\n24: 28657\r\n25: 46368\r\n26: 75025\r\n27: 121393\r\n28: 196418\r\n29: 317811\r\n30: 514229\r\n31: 832040\r\n32: 1346269\r\n33: 2178309\r\n34: 3524578\r\n35: 5702887\r\n36: 9227465\r\n37: 14930352\r\n38: 24157817\r\n39: 39088169\r\n40: 63245986\r\n41: 102334155\r\n42: 165580141\r\n43: 267914296\r\n44: 433494437\r\n45: 701408733\r\n46: 1134903170\r\n47: 1836311903\r\n48: 2971215073\r\n49: 4807526976\r\n50: 7778742049\r\n51: 12586269025\r\n52: 20365011074\r\n53: 32951280099\r\n54: 53316291173\r\n55: 86267571272\r\n56: 139583862445\r\n57: 225851433717\r\n58: 365435296162\r\n59: 591286729879\r\n60: 956722026041\r\n61: 1548008755920\r\n62: 2504730781961\r\n63: 4052739537881\r\n64: 6557470319842\r\n65: 10610209857723\r\n66: 17167680177565\r\n67: 27777890035288\r\n68: 44945570212853\r\n69: 72723460248141\r\n70: 117669030460994\r\n71: 190392490709135\r\n72: 308061521170129\r\n73: 498454011879264\r\n74: 806515533049393\r\n75: 1304969544928657\r\n76: 2111485077978050\r\n77: 3416454622906707\r\n78: 5527939700884757\r\n79: 8944394323791464\r\n80: 14472334024676221\r\n81: 23416728348467685\r\n82: 37889062373143906\r\n83: 61305790721611591\r\n84: 99194853094755497\r\n85: 160500643816367088\r\n86: 259695496911122585\r\n87: 420196140727489673\r\n88: 679891637638612258\r\n89: 1100087778366101931\r\n90: 1779979416004714189\r\n91: 2880067194370816120\r\n92: 4660046610375530309\r\n93: 7540113804746346429\r\n94: 12200160415121876738\r\n95: 19740274219868223167\r\n96: 31940434634990099905\r\n97: 51680708854858323072\r\n98: 83621143489848422977\r\n99: 135301852344706746049\r\n100: 218922995834555169026\r\n";
            Assert.That(fib100, Is.EqualTo(testFib2.ReadToEnd()));
        }

        [Test]
        public void LoadFibonacciNumbersFirst5Test()
        {
            FibonacciTextReader testFib3 = new(5); // create FibonacciTextReader with max of 5 lines
            string fib5 = "1: 0\r\n2: 1\r\n3: 1\r\n4: 2\r\n5: 3\r\n";
            Assert.That(fib5, Is.EqualTo(testFib3.ReadToEnd()));
        }
    }
}