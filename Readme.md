<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/E4914/Default.aspx) (VB: [Default.aspx](./VB/E4914/Default.aspx))
* [Default.aspx.cs](./CS/E4914/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/E4914/Default.aspx.vb))
<!-- default file list end -->
# How to implement the "Find" and "Find Next" searching functionality in ASPxGridView
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e4914/)**
<!-- run online end -->


<p><strong>UPDATED:<br /></strong><br />Starting with version 14.2, GridView provides the built-in Search / Find Panel functionality with the capability to locate it outside grid boundaries. This allows accomplishing a similar task with less effort and does not require so much extra code. See the <a href="https://community.devexpress.com/blogs/aspnet/archive/2014/11/19/asp-net-data-grid-enhancements-coming-soon-in-v14-2.aspx">ASP.NET Data Grid: Enhancements</a> post to learn more about this new functionality.<br /><br />This example demonstrates how to implement the searching functionality in ASPxGridView.</p>
<br />
<p><strong>Note</strong> that this approach has some limitations:</p>
<p>- It works only for rows that are currently visible in the GridView. So, it works only for expanded group rows.</p>
<p>- It performs searching only in cell values. So, it will not work for cells with a custom text or for the ComboBox column's TextField values.</p>
<p>- As the String.Contains method is used for search, the search is case-sensitive.</p>
<p><strong>See also:</strong><strong><br /> </strong><a href="https://www.devexpress.com/Support/Center/p/E2408">ASPxGridView - creating external filter with the ASPxTextBox and highlighting search text</a></p>

<br/>


