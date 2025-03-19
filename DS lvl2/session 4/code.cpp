#include<bits-stdc++.h>
using namespace std;

const int OO = INT_MAX;
// u,v,w
// adj[u]={v,w}
void dijkstra(int source, vector<vector<pair<int, int>>>& adj, vector<int>& dist, vector<int>& parent) {

    priority_queue<pair<int, int>>pq;// dist , node 
    vector<bool>vis(adj.size() + 5, 0); // node

    pq.push({ 0,source });

    while (!pq.empty()) {
        auto current = pq.top();
        pq.pop();
        if (vis[current.second])continue;
        vis[current.second] = 1;

        for (auto& child : adj[current.second]) {
            if (!vis[child.first] and dist[child.first] > dist[current.second] + child.second) {
                dist[child.first] = dist[current.second] + child.second;
                pq.push({ -1 * dist[child.first],child.first });
                parent[child.first] = current.second;
            }
        }
    }

}

vector<int> reconstruct_path(int source, int target, vector<int>& parent) {

    vector<int>path;
    path.push_back(target);
    int current = parent[target];

    while (current != source) {
        if (current == -1) return {};
        path.push_back(current);
        current = parent[current];

    }
    return path;

}

/////////////////////////
void bellmanFord(int V, vector<vector<pair<int, int>>>& adj, int source) {

    vector<int>dist(V + 1, OO);
    dist[source] = 0;
    vector<int>par(V, -1);
    par[source] = source;
    for (int i = 0; i < V - 1; i++) {
        for (int j = 1; j <= V; j++) {
            for (auto& p : adj[j]) {
                if (dist[p.first] > dist[j] + p.second) {
                    dist[p.first] = dist[j] + p.second;
                    par[p.first] = j;
                }
            }
        }
    }

    for (int j = 1; j <= V; j++) {
        for (auto& p : adj[j]) {
            if (dist[p.first] > dist[j] + p.second) {
                cout << "negative cycle ";
                break;
            }
        }
    }
    /////////////////

    for (int i = 1; i <= V; i++) {
        cout << "path from 1 to " << i << " " << dist[i] << endl;
    }

}
int main() {
    int n, m; // Number of nodes and edges
    cin >> n >> m;

    vector<vector<pair<int, int>>> adj(n + 5);
    for (int i = 0; i < m; i++) {
        int u, v, w;
        cin >> u >> v >> w;
        // u--, v--;
        adj[u].push_back({ v, w });
        /* adj[v].push_back({ u, w }); */
    }

    int source;
    cin >> source;
    vector<int> dist(n + 5, OO);
    vector<int> parent(n + 5, -1);
    dist[source] = 0;
    dijkstra(source, adj, dist, parent);
    parent[source] = source;
    for (int i = 1; i <= n; i++) {
        cout << "Distance from " << source << " to " << i << " is " << dist[i] << endl;
        vector<int> path = reconstruct_path(source, i, parent);
        if (!path.empty()) {
            cout << "Path: ";
            for (int node : path) {
                cout << node << " ";
            }
            cout << endl;
        }
        else {
            cout << "No path found" << endl;
        }
    }

    //////////////////////////////////

    int V, E;
    cout << "Enter number of vertices and edges: ";
    cin >> V >> E;

    vector<vector<pair<int, int>>> adj(V + 1);

    cout << "Enter edges (u v weight):\n";
    for (int i = 0; i < E; i++) {
        int u, v, weight;
        cin >> u >> v >> weight;
        adj[u].push_back({ v, weight });
    }

    int source;
    cout << "Enter source vertex: ";
    cin >> source;

    bellmanFord(V, adj, source);

    return 0;
}