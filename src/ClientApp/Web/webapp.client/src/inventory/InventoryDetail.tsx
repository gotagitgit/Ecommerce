import { Stack, TextField } from "@mui/material"
export const InventoryDetail = () => 
{
    return (
        <Stack spacing={2}>
            <TextField id="inventory-name" label="Name" variant="outlined" />
            <TextField id="inventory-stock" label="Stock" variant="outlined" />
        </Stack>
    );
}