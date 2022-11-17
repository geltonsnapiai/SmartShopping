import { useState } from "react";
import { useSelector } from "react-redux";
import { UploadListItem } from "./UploadListItem";

export const UploadList = (props) => {
    const uploadList = useSelector(store => store.uploadList);

    return (
        <div className="container-fluid pt-4 px-4">
            <div className="row g-4">
                {uploadList.map((item, index) => <UploadListItem item={item} key={index} shops={props.shops}/>)}
            </div>
        </div>
    );
}