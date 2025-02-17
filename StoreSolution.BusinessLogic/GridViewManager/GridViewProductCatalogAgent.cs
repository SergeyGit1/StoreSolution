﻿using System.Linq;
using System.Web.UI.WebControls;
using StoreSolution.BusinessLogic.Currency.Contracts;
using StoreSolution.BusinessLogic.Database.Contracts;
using StoreSolution.BusinessLogic.Database.Models;
using StoreSolution.BusinessLogic.GridViewManager.Contracts;
using StoreSolution.BusinessLogic.Lang.Contracts;
using StoreSolution.BusinessLogic.OrderRepository.Contracts;

namespace StoreSolution.BusinessLogic.GridViewManager
{
    public class GridViewProductCatalogAgent<T> : GridViewAgent<Product, T>, IGridViewProductCatalogManager<T>
    {
        private readonly IDbProductRepository _dbProductRepository;
        private readonly IOrderRepository<T> _orderRepository;
        private readonly ILangSetter _langSetter;

        public GridViewProductCatalogAgent(IDbProductRepository dbProductRepository,
            IOrderRepository<T> orderRepository, ILangSetter langSetter,
            IStorageService<T> storageService, ICurrencyConverter currencyConverter)
            : base(storageService, currencyConverter)
        {
            _dbProductRepository = dbProductRepository;
            _orderRepository = orderRepository;
            _langSetter = langSetter;
        }

        public void FillOrderColumn(GridView table, int columnIndex, int indexIdColumn, T repository)
        {
            var orders = _orderRepository.GetAll(repository);

            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = GetIdFromRow(table, i, indexIdColumn);
                var foo = orders.Find(order => order.Id == id);
                table.Rows[i].Cells[columnIndex].Text = (foo == null ? 0 : foo.Count).ToString();
            }
        }

        public void FillCategories(DropDownList ddl, IQueryable<Product> data)
        {
            var categories = data.Select(p => p.Category).Distinct().ToArray();

            var ddlSearchCategorySelectedIndex = ddl.SelectedIndex;

            ddl.Items.Clear();

            ddl.Items.Add(_langSetter.Set("ProductCatalog_AllCategories"));
            foreach (var category in categories)
                ddl.Items.Add(category);

            ddl.SelectedIndex = ddlSearchCategorySelectedIndex < ddl.Items.Count
                ? ddlSearchCategorySelectedIndex
                : ddl.Items.Count - 1;
        }

        public int GetIdFromRow(GridView table, int indexRow, int indexIdColumn)
        {
            if (indexRow < 0 || indexRow >= table.Rows.Count) return -1;

            int id;
            if (!int.TryParse(table.Rows[indexRow].Cells[indexIdColumn].Text, out id)) return -1;

            var product = _dbProductRepository.GetById(id);
            return product == null ? -1 : product.Id;
        }
    }
}