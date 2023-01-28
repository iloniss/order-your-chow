import { FC, ChangeEvent, useState } from 'react';
import PropTypes from 'prop-types';
import {
  Tooltip,
  Divider,
  Box,
  Card,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TablePagination,
  TableRow,
  TableContainer,
  Typography,
  useTheme
} from '@mui/material';
import { ProductCategory } from 'src/models/product_category';
import EditTwoToneIcon from '@mui/icons-material/EditTwoTone';
import DeleteTwoToneIcon from '@mui/icons-material/DeleteTwoTone';
import categoryService from 'src/services/categoryService';
import DeleteDialog from 'src/components/DeleteDialog/DeleteDialog';

interface CategoriesTableProps {
  className?: string;
  productCategories: ProductCategory[];
  setProductCategories: React.Dispatch<React.SetStateAction<ProductCategory[]>>;
}

const applyPagination = (
  productCategories: ProductCategory[],
  page: number,
  limit: number
): ProductCategory[] => {
  return productCategories.slice(page * limit, page * limit + limit);
};

const CategoriesTable: FC<CategoriesTableProps> = ({
  productCategories,
  setProductCategories
}) => {
  const [page, setPage] = useState<number>(0);
  const [limit, setLimit] = useState<number>(5);
  const [open, setOpen] = useState(false);
  const [productCategoryId, setProductCategoryId] = useState(0);

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  const handleLimitChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setLimit(parseInt(event.target.value));
  };

  const handleClickOpen = async (id: number) => {
    setProductCategoryId(id);
    setOpen(true);
  };

  var paginatedProducts = applyPagination(productCategories, page, limit);
  const theme = useTheme();

  const deleteCategory = async (productCategoryId: number) => {
    var result = await categoryService.deleteCategory(productCategoryId);
    if (result == null) {
      setProductCategories(
        productCategories.filter(
          (productCategories) =>
            productCategoryId !== productCategories.productCategoryId
        )
      );
    } else {
      alert(result);
    }
    setOpen(false);
  };

  return (
    <Card>
      <Divider />
      <TableContainer>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>ID Kategorii</TableCell>
              <TableCell>Kategoria</TableCell>
              <TableCell align="right">Akcje</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {paginatedProducts.map((productCategories) => {
              return (
                <TableRow hover key={productCategories.productCategoryId}>
                  <TableCell>
                    <Typography
                      variant="body1"
                      fontWeight="bold"
                      color="text.primary"
                      gutterBottom
                      noWrap
                    >
                      {productCategories.productCategoryId}
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
                      {productCategories.name}
                    </Typography>
                  </TableCell>
                  <TableCell align="right">
                    <Tooltip title="Edit Category" arrow>
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
                          '/category/edit?productcategoryid=' +
                          productCategories.productCategoryId.toString()
                        }
                      >
                        <EditTwoToneIcon fontSize="small" />
                      </IconButton>
                    </Tooltip>
                    <Tooltip title="Delete Category" arrow>
                      <IconButton
                        onClick={async () => {
                          await handleClickOpen(
                            productCategories.productCategoryId
                          );
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
        itemId={productCategoryId}
        open={open}
        setOpen={setOpen}
        deleteItem={deleteCategory}
        itemName={'kategorię'}
      />
      <Box p={2}>
        <TablePagination
          component="div"
          count={productCategories.length}
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

CategoriesTable.propTypes = {
  productCategories: PropTypes.array.isRequired
};

CategoriesTable.defaultProps = {
  productCategories: []
};

export default CategoriesTable;
