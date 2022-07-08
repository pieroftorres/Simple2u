using Simple2u.Config;

namespace Simple2u.PageObjects
{
    public abstract class BasePage
    {
        protected readonly SeleniumHelper _helper;

        protected BasePage(SeleniumHelper helper) => _helper = helper;
    }
}