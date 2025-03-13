#include<bits/stdc++.h>

using namespace std;

vector<int>dsu;

int par(int u) {
	if (dsu[u] < 0) return u;
	return dsu[u] = par(dsu[u]);
}

void unite (int u, int v) {
	int paru = par(u);
	int parv = par(v);

	if(paru == parv)return;
	if (dsu[paru] < dsu[parv]) {
		dsu[paru]+= dsu[parv];
		dsu[parv] = paru;
	}
	else {
		dsu[parv]+= dsu[paru];
		dsu[paru] = parv;
	
	}
}
struct Edge {
	int u, v, weight;

	bool operator<(const Edge& other) const {
		return weight < other.weight;
	}
};

vector<Edge> kruskal(int n, vector<Edge>& edges) {
	vector<Edge> mst; 

	dsu.assign(n, -1);

	sort(edges.begin(), edges.end());

	for (const Edge& e : edges) {
		if (mst.size() == n - 1)
			break;
		if (par(e.u) != par(e.v)) {
			unite(e.u, e.v);
			mst.push_back(e); 
		}
	}

	return mst;
}
//////////////////////////////////////////////////////
struct CompareEdge {
	bool operator()(const Edge& a, const Edge& b) const {
		return a.weight > b.weight; 
	}
};
vector<Edge>prim(vector<Edge>&edges, int n) {
	vector<vector<pair<int,int>>>adj(n);
	vector<Edge>mst;
	vector<bool>inMst(n, 0);
	inMst[0] = 1;
	priority_queue<Edge, vector<Edge>, CompareEdge> pq;
	for (auto e : edges) {
		adj[e.u].push_back({ e.v,e.weight });
		adj[e.v].push_back({ e.u,e.weight });
	}
	for (auto p : adj[0]) {
		pq.push({ 0,p.first,p.second });
	}

	while (!pq.empty() && mst.size() != n - 1) {
		Edge current = pq.top();
		pq.pop();
		if (inMst[current.v])continue;
		inMst[current.v] = 1;
		mst.push_back(current);
		for (auto& p : adj[current.v]) {
			if (!inMst[p.first])
				pq.push({ current.v, p.first, p.second });
		}
	}
	return mst;

}
int main()
{

	int n = 4; // Number of vertices
	vector<Edge> edges = {
		{0, 1, 10}, // Edge from 0 to 1 with weight 10
		{0, 2, 6},  // Edge from 0 to 2 with weight 6
		{0, 3, 5},  // Edge from 0 to 3 with weight 5
		{1, 3, 15}, // Edge from 1 to 3 with weight 15
		{2, 3, 4}   // Edge from 2 to 3 with weight 4
	};

	// Find the MST using Kruskal's algorithm
	vector<Edge> mst = kruskal(n, edges);

	// Print the edges of the MST
	cout << "Edges in the Minimum Spanning Tree:\n";
	for (const Edge& edge : mst) {
		cout << edge.u << " -- " << edge.v << " : " << edge.weight << "\n";
	}
	vector<Edge> mst1 = prim(edges,n);

	// Print the edges of the MST
	cout << "Edges in the Minimum Spanning Tree:\n";
	for (const Edge& edge : mst1) {
		cout << edge.u << " -- " << edge.v << " : " << edge.weight << "\n";
	}
	return 0;
}
