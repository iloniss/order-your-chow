import { Button, Dialog, DialogActions, DialogTitle } from '@mui/material';
import { FC } from 'react';

interface DeleteDialogProps {
  itemId: number;
  open: boolean;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  deleteItem: (itemId: number) => Promise<void>;
  itemName: string;
}
const DeleteDialog: FC<DeleteDialogProps> = ({
  setOpen,
  open,
  deleteItem,
  itemId,
  itemName
}) => {
  const handleDeleteItem = async () => {
    await deleteItem(itemId);
  };
  const handleClose = () => {
    setOpen(false);
  };
  return (
    <Dialog
      open={open}
      onClose={handleClose}
      aria-labelledby="alert-dialog-title"
      aria-describedby="alert-dialog-description"
    >
      <DialogTitle id="alert-dialog-title">
        {'Czy na pewno chcesz usunąć ' + itemName + '?'}
      </DialogTitle>
      <DialogActions>
        <Button onClick={handleClose}>Nie</Button>
        <Button onClick={handleDeleteItem} autoFocus>
          Tak
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default DeleteDialog;
