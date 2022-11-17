import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { authFetch } from "../../auth/AuthFetch";
import { asyncStatus } from "../AsyncStatus";

export const loadShops = createAsyncThunk(
    "shops/load",
    async (_, { signal }) => {
        return await authFetch("/api/shop", { signal })
            .then(res => res.json())
            .catch(console.error);
    }
);

export const shopsSlice = createSlice({
    name: "shops",
    initialState: {
        shops: [],
        status: asyncStatus.IDLE,
        error: null
    },
    reducers: { },
    extraReducers: builder => {
        builder
            .addCase(loadShops.pending, state => {
                state.status = asyncStatus.LOADING;
            })
            .addCase(loadShops.fulfilled, (state, action) => {
                state.status = asyncStatus.SUCCESS;
                state.shops = action.payload;
            })
            .addCase(loadShops.rejected, (state, action) => {
                state.status = asyncStatus.ERROR;
                state.error = action.error;
            })
    }
});

export const shopsSelector = store => store.shops.shops;
export const shopsStatusSelector = store => store.shops.status;
export const shopsErrorSelector = store => store.shops.error;