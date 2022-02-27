# KartOlusturucu
> ## Kredi Kartı Oluşturucu / Random Credit Card Generator

Seçilen Kart tipine göre kullanıcıya rastgele kart no, ccv ve son kullanma tarihi üreten, üretilen karta bakiye tanımlayıp bakiye üzerinden işlemlare olanak sağlayan program.
Kullanıcı MasterCard, Visa ve Amex (American Express) olmak üzere 3 çeşit kart seçebilir. Her kartın belli standartları vardır. 
Sistemde MasterCard, Visa ve American Express(Amex) olmak üzere şimdilik 3 çeşit kart mevcuttur.

Her kartın belirli bir kuralı bulunmaktadır:
* MasterCard Kuralları:
  * Kart numarası mutlaka 5 rakamı ile başlar.
  * İkinci rakam 1 ile 5 arasındadır.
  * Toplam 16 rakamdan oluşur.
  * Rakamlar 5xxx-xxxx-xxxx-xxxx şeklindedir.
  *CVV 3 rakamdan oluşur.
* Visa Kuralları:
  * Kart numarası mutlaka 4 rakamı ile başlar.
  * Toplam 16 rakamdan oluşur.
  * Rakamlar 4xxx-xxxx-xxxx-xxxx şeklindedir.
  * CVV 3 rakamdan oluşur.
* Amex Kuralları:
  * Kart numarası mutlaka 3 rakamı ile başlar
  * İkinci rakam 4 veya 7 olabilir.
  * Toplam 15 rakamdan oluşur.
  * Rakamlar 3xxx-xxxxxx-xxxxx şeklindedir.
  * CVV 4 rakamdan oluşur.
  
Kredi kartları numaraları yukarıdaki kurallara ve **ISO 2894/ANSI 4.13** standardına bağlı olarak üretilmiştir. Ayrıca kart numaraları **Luhn Algoritmasından** geçmektedir.
**Bu Program Nesne tabanlı programlama temelleri ele alınarak Fabrika tasarım deseni ile oluşturulmuştur.**

***

# KartOlusturucu
> ## Kredi Kartı Oluşturucu / Random Credit Card Generator
A program that generates a random card number, ccv, and expiration date for the user according to the selected card type, identifies the balance on the generated card, and allows you to process the balance.
The user can choose 3 types of cards: MasterCard, Visa and Amex (American Express). Each card has certain standards. 
There are currently 3 types of cards available in the system: MasterCard, Visa and American Express (Amex).

Each card has a certain rule:
* MasterCard Rules:
  * The card number must necessarily begin with the number 5.
  * The second digit is between 1 and 5.
  * It consists of 16 numbers in total.
  * Format is 5xxx-xxxx-xxxx-xxxx.
  * The CVV consists of 3 digits.
* Visa Rules:
  * The card number must necessarily begin with the number 4.
  * It consists of 16 numbers in total.
  * Format is 4xxx-xxxx-xxxx-xxxx.
  * The CVV consists of 3 digits.
* Amex Rules:
  * The card number must necessarily begin with the number 3.
  * The second digit can be 4 or 7.
  * It consists of 15 numbers in total.
  * Format is 3xxx-xxxxxx-xxxxx.
  * The CVV consists of 4 digits.

Credit card numbers are produced in accordance with the above rules and the **ISO 2894/ANSI 4.13** standard. In addition, the card numbers pass through the **Luhn Algorithm**.
**This Program has been created with a Factory design pattern by considering the basics of Object-Oriented Programming.**
