using Repositories;

namespace CollectionTracker.ViewModels;

public partial class DonationsViewModel(IDatasource datasource)
: ItemViewModel<Donation, DonationGridItem, WorkItem>(datasource)
{
    protected override bool UsesDoneList => false;

    public override DonationGridItem Convert(int index, Donation item)
    {
        return new DonationGridItem(
            item.ID,
            item.Link,
            item.Title,
            item.PriceInRSD,
            item.Currency,
            item.Price,
            item.Comment,
            item.ExternalID,
            item.Bookmarked);
    }
}