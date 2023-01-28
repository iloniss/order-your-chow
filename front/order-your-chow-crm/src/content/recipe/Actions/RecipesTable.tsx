import {
  Box,
  Card,
  Divider,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TablePagination,
  TableRow,
  Tooltip,
  Typography,
  useTheme
} from '@mui/material';
import DeleteTwoToneIcon from '@mui/icons-material/DeleteTwoTone';
import EditTwoToneIcon from '@mui/icons-material/EditTwoTone';
import { ChangeEvent, FC, useState } from 'react';
import { RecipesList } from 'src/models/recipe/recipes_list';
import recipeService from 'src/services/recipeService';
import DeleteDialog from 'src/components/DeleteDialog/DeleteDialog';

interface RecipesListProps {
  recipesList: RecipesList[];
  setRecipesList: React.Dispatch<React.SetStateAction<RecipesList[]>>;
}
const applyPagination = (
  recipesList: RecipesList[],
  page: number,
  limit: number
): RecipesList[] => {
  return recipesList.slice(page * limit, page * limit + limit);
};
const RecipesTable: FC<RecipesListProps> = ({
  recipesList,
  setRecipesList
}) => {
  const [page, setPage] = useState<number>(0);
  const [limit, setLimit] = useState<number>(5);
  const [open, setOpen] = useState<boolean>(false);
  const [recipeId, setRecipeId] = useState<number>(0);

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  const handleClickOpen = async (id: number) => {
    setRecipeId(id);
    setOpen(true);
  };

  const handleLimitChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setLimit(parseInt(event.target.value));
  };
  var paginatedRecipes = applyPagination(recipesList, page, limit);

  const theme = useTheme();

  const deleteRecipe = async (recipeId: number) => {
    var result = await recipeService.deleteRecipe(recipeId);
    if (result == null)
      setRecipesList(
        recipesList.filter((recipe) => recipe.recipeId !== recipeId)
      );
    else {
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
              <TableCell>ID Przepisu</TableCell>
              <TableCell>Przepis</TableCell>
              <TableCell align="right">Akcje</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {paginatedRecipes.map((recipes) => {
              return (
                <TableRow hover key={recipes.recipeId}>
                  <TableCell>
                    <Typography
                      variant="body1"
                      fontWeight="bold"
                      color="text.primary"
                      gutterBottom
                      noWrap
                    >
                      {recipes.recipeId}
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
                      {recipes.name}
                    </Typography>
                  </TableCell>
                  <TableCell align="right">
                    <Tooltip title="Edit Recipes" arrow>
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
                          '/recipe/edit?recipeId=' + recipes.recipeId.toString()
                        }
                      >
                        <EditTwoToneIcon fontSize="small" />
                      </IconButton>
                    </Tooltip>
                    <Tooltip title="Delete Recipes" arrow>
                      <IconButton
                        onClick={async () => {
                          await handleClickOpen(recipes.recipeId);
                        }}
                        sx={{
                          '&:hover': {
                            background: theme.colors.error.lighter
                          },
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
        itemId={recipeId}
        open={open}
        setOpen={setOpen}
        deleteItem={deleteRecipe}
        itemName={'przepis'}
      />
      <Box p={2}>
        <TablePagination
          component="div"
          count={recipesList.length}
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

export default RecipesTable;
