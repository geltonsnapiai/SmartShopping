import React, { Component } from 'react';
import SearchBar from './SearchBar/SearchBar';

export class Search extends Component {
    static displayName = Search.name;

    render() {
        return(
            <div>
                    <SearchBar placeHolder="Enter product"/>
                    <h2>Recently uploaded</h2>

            </div>



        )
    }

}