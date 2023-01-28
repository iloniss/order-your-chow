import { Menu, MenuItem } from '@mui/material';
import { useRef, useState } from 'react';
import { NavLink } from 'react-router-dom';

function HeaderMenu() {
  const ref = useRef<any>(null);
  const [isOpen, setOpen] = useState<boolean>(false);

  const handleClose = (): void => {
    setOpen(false);
  };

  return (
    <>
      <Menu anchorEl={ref.current} onClose={handleClose} open={isOpen}>
        <MenuItem sx={{ px: 3 }} component={NavLink} to="/dashboard">
          Dashboard
        </MenuItem>
      </Menu>
    </>
  );
}

export default HeaderMenu;
