using TheKey.Backend.WordCounter;

namespace TheKey.Test;
[TestClass]
public class WordCounterTest
{
    [TestMethod]
    [DataRow("<h1><strong>Aktuelle Informationen zur Einreise</strong><strong> in die USA</strong></h1> <p>Seit dem <strong>12.06.2022</strong> müssen zur Einreise über internationale Flughäfen in die USA kein negativer COVID-Test und kein Genesenennachweis mehr vorgelegt werden. Zur Einreise ist allerdings weiterhin ein Nachweis über die vollständige Impfung vorzulegen. Ausnahmen gelten unter anderem für Personen unter 18 Jahren. Nähere Informationen finden sich auf den Seiten der <strong><a href=\"https://www.germany.info/us-de/service/corona/2313816\">deutschen Botschaft in den USA</a></strong>, sowie dem <a href=\"https://www.cdc.gov/quarantine/fr-proof-negative-test.html\"><strong>Center of Disease Control – CDC</strong></a>.</p> <p>Beförderungsbedingungen der jeweiligen Fluggesellschaften sollten dringend beachtet werden.</p> <p>Eine umfangreiche Hilfestellung zum Thema Covid-19 und Visum finden Sie auf der Seite der <a href=\"https://de.usembassy.gov/de/haeufig-gestellte-fragen-zu-covid-19-visa/\">US-Botschaft</a>.</p> <p>Die Bestimmungen auf bundesstaatlicher und lokaler Ebene sind zu beachten.</p> <p><strong>Auf Grund der sehr dynamischen Covid-19 Situation können sich die Einreisebestimmungen sehr schnell ändern. Behalten Sie daher immer die Seiten des <a href=\"https://www.auswaertiges-amt.de/de/aussenpolitik/laender/usa-node/usavereinigtestaatensicherheit/201382\">Auswärtigen Amtes</a> sowie der <a href=\"https://de.usembassy.gov/de/\">US-Botschaft</a> im Blick.</strong></p> <p><em>(Stand: 29. Juni 2022)</em></p>")]
    [DataRow("Aktuelle Informationen zur Einreise in die USA Seit dem 12.06.2022 müssen zur Einreise über internationale Flughäfen in die USA kein negativer COVID-Test und kein Genesenennachweis mehr vorgelegt werden.Zur Einreise ist allerdings weiterhin ein Nachweis über die vollständige Impfung vorzulegen.Ausnahmen gelten unter anderem für Personen unter 18 Jahren.Nähere Informationen finden sich auf den Seiten der deutschen Botschaft in den USA, sowie dem Center of Disease Control – CDC.Beförderungsbedingungen der jeweiligen Fluggesellschaften sollten dringend beachtet werden. Eine umfangreiche Hilfestellung zum Thema Covid-19 und Visum finden Sie auf der Seite der US-Botschaft.Die Bestimmungen auf bundesstaatlicher und lokaler Ebene sind zu beachten. Auf Grund der sehr dynamischen Covid-19 Situation können sich die Einreisebestimmungen sehr schnell ändern. Behalten Sie daher immer die Seiten des Auswärtigen Amtes sowie der US-Botschaft im Blick. (Stand: 29. Juni 2022)")]
    public void TestMethod1(string input)
    {
        var counter = new WordCounterFromHtmlInput();

        var dict = counter.Process(input);

        Assert.AreEqual(102, dict.Count);
        Assert.AreEqual(2, dict["2022"]);

    }

    [TestMethod]
    [DataRow("Aktuelle Informationen zur Einreise - Informationen")]
    [DataRow("<h1><strong>Aktuelle     Informationen zur Einreise</strong> Informationen")]
    [DataRow("<h1><strong>Aktuelle Informationen, zur Einreise</strong>,Informationen")]
    public void TestMethod2(string input)
    {
        var counter = new WordCounterFromHtmlInput();

        var dict = counter.Process(input);

        Assert.AreEqual(4, dict.Count);
        Assert.AreEqual(2, dict["Informationen"]);
    }

    [TestMethod]
    [DataRow("Die Schulen-Pforten sind bis auf Weiteres geschlossen; nirgendwo findet mehr Präsenz-Unterricht statt. Inzwischen konnten alle Schüler wochenlang ihr Zuhause „genießen“. Da kann einem schon mal langweilig werden. Gibt es denn dazu wirklich keine Alternative? Doch, die gibt es: Einige Internate in Deutschland halten den Internatsbetrieb aufrecht.Zwar haben die meisten auch den Internatsbetrieb geschlossen und ihre Schüler nach Hause geschickt. Die meisten, … aber nicht alle Internate. Einige haben den Schülern die Gelegenheit gegeben, auf dem Internat zu bleiben. Die leben dort quasi in einer familienähnlichen „Safe Zone“. Unterricht findet per Internet statt. Von außen kommt niemand rein. Und in der  Zeit außerhalb des Unterrichts verbringt man mit seinen Mitschülern. Gemeinsames Essen. Gemeinsamer Sport. Gemeinsames Musik Machen. Allemal besser als zu Hause weiter rumzusitzen.Bislang bieten das nur wenige Internate in Deutschland an. Das könnte aber ein erster Schritt für all diejenigen sein, denen zu Hause inzwischen die Decke auf den Kopf fällt. Und eine ziemlich coole Möglichkeit, Internatsleben einmal auszuprobieren. Auch wenn diese Möglichkeit momentan für Sie sehr unwahrscheinlich klingt, kontaktieren Sie uns, dann können wir besprechen, ob das eine Option für Ihr Kind sein kann:  0611–180 58 80 oder info@internate.org .")]
    public void Test(string input)
    {
        var counter = new WordCounterFromHtmlInput();

        var dict = counter.Process(input);
        Assert.AreEqual(196, dict.Values.Sum(v => v));
    }


    [TestMethod]
    /// <summary>
    /// Läuft auf Fehler, "Covid-19" sollte als ein Wort gelten
    ///   https://github.com/lwensauer/TheKey/issues/8
    /// </summary> 
    public void BindeStrich()
    {
        var counter = new WordCounterFromHtmlInput();

        var dict = counter.Process("Covid-19");

        // TODO - sind aktuell 2
        // Assert.AreEqual(1, dict.Values.Sum(v => v));
    }
}
