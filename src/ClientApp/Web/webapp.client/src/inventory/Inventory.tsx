import { Stack } from "@mui/material";
import { InventoryDetail } from "./InventoryDetail";
import { InventoryList } from "./InventoryLIst";

export const Inventory = () =>
{
    return (
    <Stack>
        <h1>Inventory</h1>
        <InventoryDetail />
        <InventoryList />
    </Stack>);
}