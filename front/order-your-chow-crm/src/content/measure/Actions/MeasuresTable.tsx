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
import { ProductMeasure } from 'src/models/product_measure';
import EditTwoToneIcon from '@mui/icons-material/EditTwoTone';
import DeleteTwoToneIcon from '@mui/icons-material/DeleteTwoTone';
import measureService from 'src/services/measureService';
import DeleteDialog from 'src/components/DeleteDialog/DeleteDialog';

interface MeasuresTableProps {
  className?: string;
  productMeasures: ProductMeasure[];
  setProductMeasures: React.Dispatch<React.SetStateAction<ProductMeasure[]>>;
}

const applyPagination = (
  productMeasures: ProductMeasure[],
  page: number,
  limit: number
): ProductMeasure[] => {
  return productMeasures.slice(page * limit, page * limit + limit);
};

const MeasuresTable: FC<MeasuresTableProps> = ({
  productMeasures,
  setProductMeasures
}) => {
  const [page, setPage] = useState<number>(0);
  const [limit, setLimit] = useState<number>(5);
  const [open, setOpen] = useState(false);
  const [productMeasureId, setProductMeasureId] = useState(0);

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  const handleLimitChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setLimit(parseInt(event.target.value));
  };

  const handleClickOpen = async (id: number) => {
    setProductMeasureId(id);
    setOpen(true);
  };

  var paginatedProducts = applyPagination(productMeasures, page, limit);
  const theme = useTheme();

  const deleteMeasure = async (productMeasureId: number) => {
    var result = await measureService.deleteMeasure(productMeasureId);
    if (result == null) {
      setProductMeasures(
        productMeasures.filter(
          (productMeasures) =>
            productMeasureId !== productMeasures.productMeasureId
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
              <TableCell>ID Miary</TableCell>
              <TableCell>Jednostka miary</TableCell>
              <TableCell align="right">Akcje</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {paginatedProducts.map((productMeasures) => {
              return (
                <TableRow hover key={productMeasures.productMeasureId}>
                  <TableCell>
                    <Typography
                      variant="body1"
                      fontWeight="bold"
                      color="text.primary"
                      gutterBottom
                      noWrap
                    >
                      {productMeasures.productMeasureId}
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
                      {productMeasures.name}
                    </Typography>
                  </TableCell>
                  <TableCell align="right">
                    <Tooltip title="Edit Measure" arrow>
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
                          '/measure/edit?productmeasureid=' +
                          productMeasures.productMeasureId.toString()
                        }
                      >
                        <EditTwoToneIcon fontSize="small" />
                      </IconButton>
                    </Tooltip>
                    <Tooltip title="Delete Measure" arrow>
                      <IconButton
                        onClick={async () => {
                          await handleClickOpen(
                            productMeasures.productMeasureId
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
        itemId={productMeasureId}
        open={open}
        setOpen={setOpen}
        deleteItem={deleteMeasure}
        itemName={'miarę'}
      />
      <Box p={2}>
        <TablePagination
          component="div"
          count={productMeasures.length}
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

MeasuresTable.propTypes = {
  productMeasures: PropTypes.array.isRequired
};

MeasuresTable.defaultProps = {
  productMeasures: []
};

export default MeasuresTable;
