using BlueBoxes.WordSearchBuilder.Models;
using BlueBoxes.WordSearchBuilder.WordPlacers;

namespace BlueBoxes.WordSearchBuilder.Tests.WordPlacers;

public class NorthPlacerTests
{
    [Test]
    public void WhenPlaceWordNorthSuccessfully()
    {
        var placer = new Reversed(new South());
        var wordSearch = new WordSearchBuilder(4, 4, false, "", false, false);
        var result = placer.TryPlaceWord("TEST", wordSearch.Grid);
        result.Should().NotBeNull();
        result.Should().NotBe(PlacedWord.Empty);

        result.StartCell.Row.Should().Be(3);

        wordSearch.Grid[result.StartCell.Col][result.StartCell.Row].Should().Be('T');
        wordSearch.Grid[result.StartCell.Col][result.StartCell.Row - 1].Should().Be('E');
        wordSearch.Grid[result.StartCell.Col][result.StartCell.Row - 2].Should().Be('S');
        wordSearch.Grid[result.StartCell.Col][result.StartCell.Row - 3].Should().Be('T');
    }

    [Test]
    public void WhenPlaceWordInPopulatedGridSuccessfully()
    {
        var placer = new Reversed(new South());
        var wordSearch = new WordSearchBuilder(4, 4, false, "", false, false);
        wordSearch.Grid[0][0] = 'P';
        wordSearch.Grid[1][1] = 'S';
        wordSearch.Grid[2][2] = 'Z';
        wordSearch.Grid[3][3] = 'L';
        var result = placer.TryPlaceWord("TEST", wordSearch.Grid);
        result.Should().NotBeNull();
        result.Should().NotBe(PlacedWord.Empty);

        result.StartCell.Col.Should().Be(1);
        result.StartCell.Row.Should().Be(3);

        wordSearch.Grid[1][0].Should().Be('T');
        wordSearch.Grid[1][1].Should().Be('S');
        wordSearch.Grid[1][2].Should().Be('E');
        wordSearch.Grid[1][3].Should().Be('T');
    }

    [Test]
    public void WhenPlaceWordInSmallGridFail()
    {
        var placer = new Reversed(new South());
        var wordSearch = new WordSearchBuilder(3, 3, false, "", false, false);
        var result = placer.TryPlaceWord("TEST", wordSearch.Grid);
        result.Should().Be(PlacedWord.Empty);
    }

    [Test]
    public void WhenPlaceWordInPopulatedGridFail()
    {
        var placer = new Reversed(new South());
        var wordSearch = new WordSearchBuilder(4, 4, false, "", false, false);
        wordSearch.Grid[0][0] = 'T';
        wordSearch.Grid[1][1] = 'E';
        wordSearch.Grid[2][2] = 'S';
        wordSearch.Grid[3][3] = 'T';
        var result = placer.TryPlaceWord("LOAN", wordSearch.Grid);
        result.Should().Be(PlacedWord.Empty);
    }
}