import { FC, ChangeEvent, useState } from 'react';
import PropTypes from 'prop-types';
import {
  Tooltip,
  Divider,
  Box,
  FormControl,
  InputLabel,
  Card,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TablePagination,
  TableRow,
  TableContainer,
  Select,
  MenuItem,
  Typography,
  useTheme,
  CardHeader
} from '@mui/material';

import { Product } from 'src/models/product';
import { ProductCategory } from 'src/models/product_category';
import EditTwoToneIcon from '@mui/icons-material/EditTwoTone';
import DeleteTwoToneIcon from '@mui/icons-material/DeleteTwoTone';
import ProductService from './../../../services/productService';
import DeleteDialog from 'src/components/DeleteDialog/DeleteDialog';

interface ProductsTableProps {
  className?: string;
  products: Product[];
  productCategories: ProductCategory[];
  setProducts: React.Dispatch<React.SetStateAction<Product[]>>;
}

interface Filters {
  category?: string;
}

const applyFilters = (products: Product[], filters: Filters): Product[] => {
  return products.filter((product) => {
    let matches = true;

    if (
      filters.category &&
      product.productCategoryId.toString() !== filters.category
    ) {
      matches = false;
    }

    return matches;
  });
};

const applyPagination = (
  products: Product[],
  page: number,
  limit: number
): Product[] => {
  return products.slice(page * limit, page * limit + limit);
};

const ProductsTable: FC<ProductsTableProps> = ({
  products,
  setProducts,
  productCategories
}) => {
  const [page, setPage] = useState<number>(0);
  const [limit, setLimit] = useState<number>(5);
  const [filters, setFilters] = useState<Filters>({
    category: null
  });
  const [open, setOpen] = useState(false);
  const [productId, setProductId] = useState(0);
  const categoryOptions = [
    {
      id: '0',
      name: 'Wszystkie'
    }
  ];

  productCategories.forEach((element) => {
    categoryOptions.push({
      id: element.productCategoryId.toString(),
      name: element.name
    });
  });

  const handleCategoryChange = (e: ChangeEvent<HTMLInputElement>): void => {
    let value = null;

    if (e.target.value !== '0') {
      value = e.target.value;
    }

    setFilters((prevFilters) => ({
      ...prevFilters,
      category: value
    }));
  };

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  const handleLimitChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setLimit(parseInt(event.target.value));
  };

  const handleClickOpen = async (id: number) => {
    setProductId(id);
    setOpen(true);
  };

  const deleteProduct = async (productId: number) => {
    var result = await ProductService.deleteProduct(productId);
    if (result == null) {
      setProducts(
        products.filter((product) => productId !== product.productId)
      );
    } else {
      alert(result);
    }
    setOpen(false);
  };

  const filteredProducts = applyFilters(products, filters);
  var paginatedProducts = applyPagination(filteredProducts, page, limit);
  const theme = useTheme();

  return (
    <Card>
      <CardHeader
        action={
          <Box width={200}>
            <FormControl fullWidth variant="outlined">
              <InputLabel>Kategoria</InputLabel>
              <Select
                value={filters.category || '0'}
                onChange={handleCategoryChange}
                label="Kategoria"
                autoWidth
              >
                {categoryOptions.map((categoryOption) => (
                  <MenuItem key={categoryOption.id} value={categoryOption.id}>
                    {categoryOption.name}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          </Box>
        }
      />
      <Divider />
      <TableContainer>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Produkt ID</TableCell>
              <TableCell>Produkt</TableCell>
              <TableCell>Kategoria</TableCell>
              <TableCell align="right">Akcje</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {paginatedProducts.map((product) => {
              return (
                <TableRow hover key={product.productId}>
                  <TableCell>
                    <Typography
                      variant="body1"
                      fontWeight="bold"
                      color="text.primary"
                      gutterBottom
                      noWrap
                    >
                      {product.productId}
                    </Typography>
                  </TableCell>
                  <TableCell>
                    <Typography
                      variant="body1"
                      fontWeight="bold"
                      color="text.primary"
                      gutterBottom
                      noWrap
                    >
                      {product.name}
                    </Typography>
                  </TableCell>
                  <TableCell>
                    <Typography
                      variant="body1"
                      fontWeight="bold"
                      color="text.primary"
                      gutterBottom
                      noWrap
                    >
                      {product.productCategory}
                    </Typography>
                  </TableCell>
                  <TableCell align="right">
                    <Tooltip title="Edit Product" arrow>
                      <IconButton
                        sx={{
                          '&:hover': {
                            background: theme.colors.primary.lighter
                          },
                          color: theme.palette.primary.main
                        }}
                        color="inherit"
                        size="small"
                        href={
                          '/product/edit?productid=' +
                          product.productId.toString()
                        }
                      >
                        <EditTwoToneIcon fontSize="small" />
                      </IconButton>
                    </Tooltip>
                    <Tooltip title="Delete Product" arrow>
                      <IconButton
                        onClick={async () => {
                          await handleClickOpen(product.productId);
                        }}
                        sx={{
                          '&:hover': { background: theme.colors.error.lighter },
                          color: theme.palette.error.main
                        }}
                        color="inherit"
                        size="small"
                      >
                        <DeleteTwoToneIcon fontSize="small" />
                      </IconButton>
                    </Tooltip>
                  </TableCell>
                </TableRow>
              );
            })}
          </TableBody>
        </Table>
      </TableContainer>
      <DeleteDialog
        itemId={productId}
        open={open}
        setOpen={setOpen}
        deleteItem={deleteProduct}
        itemName={'produkt'}
      />
      <Box p={2}>
        <TablePagination
          component="div"
          count={filteredProducts.length}
          onPageChange={handlePageChange}
          onRowsPerPageChange={handleLimitChange}
          page={page}
          rowsPerPage={limit}
          rowsPerPageOptions={[5, 10, 25, 30]}
        />
      </Box>
    </Card>
  );
};

ProductsTable.propTypes = {
  products: PropTypes.array.isRequired
};

ProductsTable.defaultProps = {
  products: []
};

export default ProductsTable;
